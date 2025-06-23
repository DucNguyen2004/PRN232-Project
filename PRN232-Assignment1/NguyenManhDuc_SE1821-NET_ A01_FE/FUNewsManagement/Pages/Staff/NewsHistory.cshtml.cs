using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNewsManagement.Pages.Staff
{
    public class NewsHistoryModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public NewsHistoryModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<NewsArticle> NewsArticles { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToPage("/Account/Login");

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{Constant.LocalHost}/api/News/creator/{userId}");

            if (response.IsSuccessStatusCode)
            {
                NewsArticles = await response.Content.ReadFromJsonAsync<List<NewsArticle>>() ?? new();
            }
            else
            {
                ModelState.AddModelError("", "Failed to load news history.");
            }

            return Page();
        }
    }
}
