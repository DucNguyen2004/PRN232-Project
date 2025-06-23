using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNewsManagement.Pages.News
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<NewsArticle> NewsList { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public short? CategoryId { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var query = new List<string>();
            if (CategoryId.HasValue)
                query.Add($"categoryId={CategoryId.Value}");

            var queryString = string.Join("&", query);
            var newsUrl = $"{Constant.LocalHost}/api/News";
            if (!string.IsNullOrEmpty(queryString))
                newsUrl += "?" + queryString;

            var newsTask = client.GetFromJsonAsync<List<NewsArticle>>(newsUrl);
            var categoryTask = client.GetFromJsonAsync<List<Category>>($"{Constant.LocalHost}/api/Category");
            var tagTask = client.GetFromJsonAsync<List<Tag>>($"{Constant.LocalHost}/api/Tag");

            await Task.WhenAll(newsTask, categoryTask, tagTask);

            NewsList = newsTask.Result ?? new();
            Categories = categoryTask.Result ?? new();
            Tags = tagTask.Result ?? new();

            return Page();
        }
    }
}
