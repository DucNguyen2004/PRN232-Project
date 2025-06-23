using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNewsManagement.Pages.Staff
{
    public class DashboardModel : PageModel
    {
        public bool IsStaff { get; private set; }

        public void OnGet()
        {
            IsStaff = HttpContext.Session.GetInt32("UserRole") == 1;
            if (!IsStaff)
            {
                Response.Redirect("/Account/Login");
            }
        }
    }
}

