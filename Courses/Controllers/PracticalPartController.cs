using Azure;
using Courses.Domain.Entity;
using Courses.Domain.ViewModules;
using Courses.Service.Implementations;
using Courses.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Courses.Controllers
{
    public class PracticalPartController : Controller
    {
        private readonly IPracticalPartService _practicalPartService;
        private readonly ICompletedPartService _completedPartService;
        private readonly UserManager<User> _userManager;
        public PracticalPartController(IPracticalPartService practicalPartService,
                                       ICompletedPartService completedPartService,
                                       UserManager<User> userManager)
        {
            _practicalPartService = practicalPartService;
            _completedPartService = completedPartService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetPracticalParts(int courseId)
        {
            var response = await _practicalPartService.GetPracticalParts(courseId);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return View(response.Data);
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetPracticalPart(int courseId, int number)
        {
            var response = await _practicalPartService.GetPracticalPart(courseId, number);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return View(response.Data);
            return RedirectToAction("Error");
        }

        //[HttpDelete]
        public async Task<IActionResult> Delete(int courseId, int number)
        {
            var response = await _practicalPartService.DeletePracticalPart(courseId, number);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return View(response.Data);
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Save(int courseId, int number)
        {
            if (number == 0)
                return View();
            var response = await _practicalPartService.GetPracticalPart(courseId, number);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                return View(response.Data);
            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Save(PracticalPartViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _practicalPartService.CreatePracticalPart(model);
                
                }
                else
                {
                    _practicalPartService.Edit(model.CourseId, model.Id, model);
                }
                return View();
            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<bool> CheckAnswer(string partId, string courseId, string answer)
        {
            var response = await _practicalPartService.CheckAnswer(int.Parse(courseId), int.Parse(partId), answer );
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                if (response.Data)
                {
                    var createCCS = await _completedPartService.CreateComletedPart(int.Parse(partId), _userManager.GetUserId(HttpContext.User));
                    if (createCCS.StatusCode == Domain.Enum.StatusCode.OK)
                        return true;
                }
                return false;
            }
            return false;
        }
    }
}
