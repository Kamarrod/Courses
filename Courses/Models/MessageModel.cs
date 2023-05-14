using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesApp.Pages
{
    public class CourseIdModel : PageModel
    {
        public int Message { get; private set; } = 0;
        public void OnGet(int name)
        {
            Message = name;
        }
    }
}