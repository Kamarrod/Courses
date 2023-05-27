using Courses.Domain.Entity;
using Courses.Domain.ViewModules.Course;
using Courses.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Courses.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly UserManager<User> _userManager;
        public CourseController(UserManager<User> userManager, ICourseService courseService)
        {
            _userManager = userManager;
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            var response = await _courseService.GetCourses();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return View(response.Data);
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetCourse(int id)
        {
            if (id == 0)
            {
                id = int.Parse(HttpContext.Session.GetString("CourseId"));
            }
            var response = await _courseService.GetCourse(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return View(response.Data);
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthorCourses()
        {
            var response = await _courseService.GetAuthorCourses(_userManager.GetUserId(HttpContext.User));
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return View(response.Data);
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetCoursesWithSimilarName(string name)
        {
            var response = await _courseService.GetCoursesWithSimilarName(name);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return View( "GetCourses", response.Data);
            return RedirectToAction("Error");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (_courseService.GetCourse(id).Result.Data.AuthorId == _userManager.GetUserId(HttpContext.User) || User.IsInRole("Admin") || User.IsInRole("Moderator"))
            {
                var response = await _courseService.DeleteCourse(id);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return View("GetAuthorCourses");
                }
                return RedirectToAction("Error");
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {          
            if (id == 0)
            {
                return View();
            }

            if(id != 0 && _userManager.GetUserId(HttpContext.User) == _courseService.GetCourse(id).Result.Data.AuthorId)
            {
                var response = await _courseService.GetCourse(id);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                    return View(response.Data);
                return RedirectToAction("Error");
            }

            return RedirectToAction("Error");

           
        }

        [HttpPost]
        public async Task<IActionResult> Save(CourseViewModel model)
        {
            model.AuthorId = _userManager.GetUserId(HttpContext.User);
            ModelState.Remove("AuthorId");
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _courseService.CreateCourse(model);
                }
                else
                {
                    _courseService.Edit(model.Id, model);
                }
                return RedirectToAction("GetCourses");
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public JsonResult GetTypes()
        {
            var types = _courseService.GetTypes();
            return Json(types.Data);
        }
    }
}
