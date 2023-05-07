using Courses.DAL.Interfaces;
using Courses.Domain.Entity;
using Courses.Models;
using Courses.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using System.Text.Json;

namespace Courses.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly ICompletedPartService _completedPartService;
        private readonly ICompletedCourseService _completedCourseService;

        public HomeController(ICompletedPartService completedPartService, UserManager<User> userManager,
                              ICompletedCourseService completedCourseService)
        {
            _completedPartService = completedPartService;
            _userManager = userManager;
            _completedCourseService = completedCourseService;
        }
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                SortedSet<int> idPCompletedParts = new SortedSet<int>();
                var listofCompletedParts = await _completedPartService.GetCompletedPartBuIdUser(_userManager.GetUserId(HttpContext.User));
                if (listofCompletedParts.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    foreach (var el in listofCompletedParts.Data)
                    {
                        idPCompletedParts.Add(el.PracticalPartId);
                    }
                    HttpContext.Session.Set("completedParts", JsonSerializer.SerializeToUtf8Bytes(idPCompletedParts));
                }

                SortedSet<int> idPCompletedCourse = new SortedSet<int>();
                var listofCompletedCourse = await _completedCourseService.GetCompletedCourseByIdUser(_userManager.GetUserId(HttpContext.User));
                if (listofCompletedCourse.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    foreach (var el in listofCompletedCourse.Data)
                    {
                        idPCompletedCourse.Add(el.CourseId);
                    }
                    HttpContext.Session.Set("completedCourse", JsonSerializer.SerializeToUtf8Bytes(idPCompletedCourse));
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}