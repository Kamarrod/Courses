using Courses.Service.Implementations;
using Courses.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Сourses.Domain.Entity;

namespace Courses.Controllers
{
    [Authorize]
    public class CompletedCourseController : Controller
    {
        private readonly ICompletedCourseService _completedCourseService;
        public CompletedCourseController(ICompletedCourseService completedCourseService)
        {
            _completedCourseService = completedCourseService;
        }
        public async Task<IActionResult> GetAllCompletedCourses(string userId)
        {
            var response = await _completedCourseService.GetCompletedCourses(userId);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return View(response.Data);
            return RedirectToAction("Error");
        }
    }
}
