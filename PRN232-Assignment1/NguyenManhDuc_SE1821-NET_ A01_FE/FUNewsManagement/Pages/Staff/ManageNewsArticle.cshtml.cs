using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FUNewsManagement.Pages.Staff
{
    public class ManageNewsArticlesModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ManageNewsArticlesModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<NewsArticle> NewsArticles { get; set; } = new();
        public List<SelectListItem> Categories { get; set; } = new();

        [BindProperty]
        public NewsArticle NewsArticle { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadDataAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    await LoadDataAsync();
            //    return Page();
            //}

            short? userId = HttpContext.Session.GetInt32("UserId") is int id ? (short)id : (short?)null;
            if (userId == null) return RedirectToPage("/Account/Login");

            NewsArticle.CreatedById = userId.Value;
            NewsArticle.UpdatedById = userId.Value;
            NewsArticle.CreatedDate = DateTime.Now;
            NewsArticle.ModifiedDate = DateTime.Now;
            NewsArticle.NewsArticleId = Guid.NewGuid().ToString();
            NewsArticle.NewsStatus = true;

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync($"{Constant.LocalHost}/api/News", NewsArticle);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to create news article.");
                await LoadDataAsync();
                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    await LoadDataAsync();
            //    return Page();
            //}

            var client = _httpClientFactory.CreateClient();
            var response = await client.PutAsJsonAsync($"{Constant.LocalHost}/api/News", NewsArticle);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to update news article.");
                await LoadDataAsync();
                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{Constant.LocalHost}/api/News/{id}");

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to delete news article.");
            }

            return RedirectToPage();
        }

        private async Task LoadDataAsync()
        {
            var client = _httpClientFactory.CreateClient();

            // Get news
            var newsResponse = await client.GetAsync($"{Constant.LocalHost}/api/News");
            if (newsResponse.IsSuccessStatusCode)
            {
                NewsArticles = await newsResponse.Content.ReadFromJsonAsync<List<NewsArticle>>() ?? new();
            }

            // Get categories
            var categoryResponse = await client.GetAsync($"{Constant.LocalHost}/api/Category");
            if (categoryResponse.IsSuccessStatusCode)
            {
                var categoryList = await categoryResponse.Content.ReadFromJsonAsync<List<Category>>() ?? new();
                Categories = categoryList.Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                }).ToList();
            }
        }
    }
}
