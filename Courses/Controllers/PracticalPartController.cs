using Courses.Domain.ViewModules;
using Courses.Service.Implementations;
using Courses.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Courses.Controllers
{
    public class PracticalPartController : Controller
    {
        private readonly IPracticalPartService _practicalPartService;

        public PracticalPartController(IPracticalPartService practicalPartService)
        {
            _practicalPartService = practicalPartService;
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
            //if (ModelState.IsValid)
            //{
                if (model.Id == 0)
                {
                    await _practicalPartService.CreatePracticalPart(model);
                }
                {
                    _practicalPartService.Edit(model.CourseId, model.Id, model);
                }
            //}
            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<bool> CheckAnswer(string partId, string courseId, string answer)
        {
            var response = await _practicalPartService.CheckAnswer(int.Parse(courseId), int.Parse(partId), answer );
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
                 return response.Data;
            return false;
        }
    }
}
