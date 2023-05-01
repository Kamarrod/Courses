using Courses.DAL.Interfaces;
using Courses.Domain.Entity;
using Courses.Domain.ViewModules.Course;
using Courses.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Сourses.Domain.Enum;

namespace Courses.Controllers
{
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
            var response = await _courseService.GetCourse(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return View(response.Data);
            return RedirectToAction("Error");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _courseService.DeleteCourse(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
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

            var response = await _courseService.GetCourse(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return View(response.Data);
            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Save(CourseViewModel model)
        {
            model.TypeCourse = "3";
            model.AuthorId = _userManager.GetUserId(HttpContext.User);

            //if (ModelState.IsValid)
            //{
                if (model.Id == 0)
                {
                    await _courseService.CreateCourse(model);
                }
                else
                {
                    _courseService.Edit(model.Id, model);
                }
            //}
            return RedirectToAction("GetCourses");
        }

        //[HttpGet]
        //public async Task<IActionResult> GetPracticalPart(CourseViewModel model)
        //{
        //    var response = await _courseService.GetPracticalPart(model);
        //    if (response.StatusCode == Domain.Enum.StatusCode.OK)
        //    {
        //        return View(response.Data);
        //    }
        //    return RedirectToAction("Error");
        //}
    }
}
