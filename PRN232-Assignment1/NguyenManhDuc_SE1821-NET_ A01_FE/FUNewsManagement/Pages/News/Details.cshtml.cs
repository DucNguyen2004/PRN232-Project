using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNewsManagement.Pages.News
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public NewsArticle News { get; set; }
        public List<Category> Categories { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var client = _httpClientFactory.CreateClient();

            // Get News
            var newsResponse = await client.GetAsync($"{Constant.LocalHost}/api/News/{id}");
            if (!newsResponse.IsSuccessStatusCode)
            {
                return NotFound();
            }
            News = await newsResponse.Content.ReadFromJsonAsync<NewsArticle>();

            // Get Categories
            var catResponse = await client.GetAsync($"{Constant.LocalHost}/api/Category");
            if (catResponse.IsSuccessStatusCode)
            {
                Categories = await catResponse.Content.ReadFromJsonAsync<List<Category>>() ?? new();
            }

            // Get Tags
            var tagResponse = await client.GetAsync($"{Constant.LocalHost}/api/Tag");
            if (tagResponse.IsSuccessStatusCode)
            {
                Tags = await tagResponse.Content.ReadFromJsonAsync<List<Tag>>() ?? new();
            }

            return Page();
        }
    }
}
