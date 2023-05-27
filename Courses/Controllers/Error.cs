using Courses.Domain.Response;
using Courses.Domain.ViewModules;
using Microsoft.AspNetCore.Mvc;

namespace Courses.Controllers
{
    public class Error : Controller
    {
        public async Task<IActionResult> GetError<T>(IBaseResponse<T> response)
        {
            var errorModel = new ErrorViewModel();
            errorModel.Desctiption = response.Description;
            errorModel.statusCode = response.StatusCode;
            return View(errorModel);
        }
    }
}
