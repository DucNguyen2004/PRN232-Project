using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNewsManagement.Pages.Staff
{
    public class ProfileModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProfileModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public SystemAccount Account { get; set; }

        [BindProperty]
        public string CurrentPassword { get; set; }

        [BindProperty]
        public string NewPassword { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            short? userId = HttpContext.Session.GetInt32("UserId") is int id ? (short)id : (short?)null;
            if (userId == null) return RedirectToPage("/Account/Login");

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{Constant.LocalHost}/api/Account/{userId}");

            if (!response.IsSuccessStatusCode)
                return RedirectToPage("/Account/Login");

            Account = await response.Content.ReadFromJsonAsync<SystemAccount>();

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateProfileAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PutAsJsonAsync($"{Constant.LocalHost}/api/Account/update", Account);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to update profile.");
                return Page();
            }

            TempData["SuccessMessage"] = "Profile updated successfully!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostChangePasswordAsync()
        {
            short? userId = HttpContext.Session.GetInt32("UserId") is int id ? (short)id : (short?)null;
            if (userId == null) return RedirectToPage("/Account/Login");

            var client = _httpClientFactory.CreateClient();

            var body = new
            {
                UserId = userId.Value,
                CurrentPassword,
                NewPassword
            };

            var response = await client.PostAsJsonAsync($"{Constant.LocalHost}/api/Account/change-password", body);

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Password changed successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Current password is incorrect.";
            }

            return RedirectToPage();
        }
    }
}
