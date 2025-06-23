using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace FUNewsManagement.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<Category> Categories { get; private set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{Constant.LocalHost}/api/categories");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                Categories = JsonSerializer.Deserialize<List<Category>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<Category>();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync(Category category)
        {
            if (!ModelState.IsValid) return await OnGetAsync();

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync($"{Constant.LocalHost}/api/categories", category);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to create category.");
                return await OnGetAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(Category category)
        {
            if (!ModelState.IsValid) return await OnGetAsync();

            var client = _httpClientFactory.CreateClient();
            var response = await client.PutAsJsonAsync($"{Constant.LocalHost}/api/categories/{category.CategoryId}", category);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to update category.");
                return await OnGetAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(short id)
        {
            var client = _httpClientFactory.CreateClient();

            // Check if news is linked to this category
            var checkResponse = await client.GetAsync($"{Constant.LocalHost}/api/categories/y/{id}");
            if (checkResponse.IsSuccessStatusCode)
            {
                var news = await checkResponse.Content.ReadFromJsonAsync<List<NewsArticle>>();
                if (news?.Any() == true)
                {
                    ModelState.AddModelError("", "Cannot delete. Category is linked to news articles.");
                    return await OnGetAsync();
                }
            }

            var deleteResponse = await client.DeleteAsync($"{Constant.LocalHost}/api/categories/{id}");

            if (!deleteResponse.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to delete category.");
                return await OnGetAsync();
            }

            return RedirectToPage();
        }
    }
}
