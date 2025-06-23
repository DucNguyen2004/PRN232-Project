using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNewsManagement.Pages.Admin
{
    public class ReportModel : PageModel
    {
        [BindProperty]
        public DateTime StartDate { get; set; }

        [BindProperty]
        public DateTime EndDate { get; set; }

        public void OnGet()
        {
            StartDate = DateTime.Today;
            EndDate = DateTime.Today;
        }
        public IActionResult OnPost()
        {
            if (HttpContext.Session.GetInt32("UserRole") != 0)
            {
                return RedirectToPage("/Account/Login");
            }

            return RedirectToPage("/Admin/ReportResults", new { startDate = StartDate, endDate = EndDate });
        }
    }
}
