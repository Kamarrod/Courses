using Courses.Domain.Entity;
using Courses.Service.Implementations;
using Courses.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Courses.Controllers
{
    [Authorize]
    public class SubscribedCourseController : Controller
    {
        private readonly ISubscribedCourseService _subscribedCourseService;
        private readonly UserManager<User> _userManager;

        public SubscribedCourseController(ISubscribedCourseService subscribedCourseService,
                                          UserManager<User> userManager)
        {
            _subscribedCourseService = subscribedCourseService;
            _userManager = userManager;
        }

        public async Task<IActionResult> DeleteSubscribedCourse(int courseId,string idUser)
        {
            var response = await _subscribedCourseService.DeleteSubscribedCourse(courseId, idUser);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return RedirectToAction("GetPracticalParts", "PracticalPart", courseId);
            return RedirectToAction("Error");
        }

        public async Task<IActionResult> SubscribeOnCoursе(int id)
        {
            var response = await _subscribedCourseService.CreateSubscribedCourse(id, _userManager.GetUserId(HttpContext.User));
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                HttpContext.Session.SetString("CourseId", id.ToString());
                return RedirectToAction("GetCourse", "Course", id);
            }
            return RedirectToAction("Error");
        }

        public async Task<IActionResult> GetAllSubscribedCourses(string userId)
        {
            var response = await _subscribedCourseService.GetSubscribedCourse(userId);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return View(response.Data);
            return RedirectToAction("Error");
        }

    }
}
