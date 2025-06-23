using BusinessObjects;
using FUNewsManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace FUNewsManagement.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public AccountViewModel AccountModel { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var client = _httpClientFactory.CreateClient();
            var json = JsonSerializer.Serialize(AccountModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{Constant.LocalHost}/api/Account/validate", content);


            if (!response.IsSuccessStatusCode)
            {
                ErrorMessage = "Invalid email or password.";
                return Page();
            }

            var resultJson = await response.Content.ReadAsStringAsync();
            var user = JsonSerializer.Deserialize<SystemAccount>(resultJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (user == null || user.AccountRole == -1)
            {
                ErrorMessage = user == null ? "Invalid email or password." : "Your account is deactivated.";
                return Page();
            }

            // Store login session
            HttpContext.Session.SetInt32("UserId", user.AccountId);
            HttpContext.Session.SetString("UserName", user.AccountName);
            HttpContext.Session.SetInt32("UserRole", user.AccountRole ??= 0);

            return user.AccountRole == 0 ? RedirectToPage("/Admin/Dashboard") : RedirectToPage("/News/Index");
        }
    }
}
