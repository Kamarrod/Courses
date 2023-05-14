using Courses.Service.Implementations;
using Courses.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Courses.Controllers
{
    public class SubscribedCourseController : Controller
    {
        private readonly ISubscribedCourseService _subscribedCourseService;

        public SubscribedCourseController(ISubscribedCourseService subscribedCourseService)
        {
            _subscribedCourseService = subscribedCourseService;
        }

        public async Task<IActionResult> DeleteSubscribedCourse(int courseId,string idUser)
        {
            var response = await _subscribedCourseService.DeleteSubscribedCourse(courseId, idUser);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return RedirectToAction("GetPracticalParts", "PracticalPart", courseId);
            return RedirectToAction("Error");
        }
    }
}
