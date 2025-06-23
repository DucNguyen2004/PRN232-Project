using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace FUNewsManagement.Pages.Admin
{
    public class ReportResultsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReportResultsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<NewsArticle> ReportData { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(DateTime startDate, DateTime endDate)
        {
            if (HttpContext.Session.GetInt32("UserRole") != 0)
            {
                return RedirectToPage("/Account/Login");
            }

            var client = _httpClientFactory.CreateClient();
            var apiUrl = $"{Constant.LocalHost}/api/report?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}";

            var response = await client.GetAsync(apiUrl);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                TempData["ErrorMessage"] = !string.IsNullOrWhiteSpace(errorContent)
                    ? errorContent
                    : "An error occurred while generating the report.";
                return Page();
            }

            var json = await response.Content.ReadAsStringAsync();
            ReportData = JsonSerializer.Deserialize<List<NewsArticle>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<NewsArticle>();

            return Page();
        }
    }
}
