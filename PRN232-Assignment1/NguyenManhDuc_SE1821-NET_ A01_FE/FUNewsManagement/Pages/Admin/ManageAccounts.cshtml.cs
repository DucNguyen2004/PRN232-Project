using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace FUNewsManagement.Pages.Admin
{
    public class ManageAccountsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ManageAccountsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<SystemAccount> Users { get; set; }
        [BindProperty]
        public SystemAccount NewUser { get; set; } = new();
        [BindProperty]
        public short AccountId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetInt32("UserRole") != 0)
            {
                return RedirectToPage("/Account/Login");
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{Constant.LocalHost}/api/Account/users");

            if (!response.IsSuccessStatusCode)
            {
                // Optional: You can redirect or show an error
                Users = new List<SystemAccount>();
                TempData["ErrorMessage"] = "Failed to load users.";
                return Page();
            }

            var json = await response.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<SystemAccount>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Users = users ?? new List<SystemAccount>();
            return Page();
        }
        public async Task<IActionResult> OnPostCreateAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync($"{Constant.LocalHost}/api/account", NewUser);

            if (!response.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "Failed to create user.";
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{Constant.LocalHost}/api/account/{AccountId}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "Failed to delete user.";
            }

            return RedirectToPage();
        }

        [BindProperty]
        public int NewRole { get; set; }

        public async Task<IActionResult> OnPostEditRoleAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(NewRole.ToString(), System.Text.Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"{Constant.LocalHost}/api/account/{AccountId}/role", content);

            if (!response.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "Failed to update user role.";
            }

            return RedirectToPage();
        }
    }
}
