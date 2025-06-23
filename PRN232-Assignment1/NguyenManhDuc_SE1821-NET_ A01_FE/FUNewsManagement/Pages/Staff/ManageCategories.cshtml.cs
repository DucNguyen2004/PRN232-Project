using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNewsManagement.Pages.Staff
{
    public class ManageCategoriesModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ManageCategoriesModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public Category Category { get; set; }

        public List<Category> Categories { get; set; } = new();

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{Constant.LocalHost}/api/Category");

            if (response.IsSuccessStatusCode)
            {
                Categories = await response.Content.ReadFromJsonAsync<List<Category>>() ?? new();
            }
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (!ModelState.IsValid) return Page();

            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync($"{Constant.LocalHost}/api/Category", Category);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to create category.");
                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid) return Page();

            var client = _httpClientFactory.CreateClient();
            var response = await client.PutAsJsonAsync($"{Constant.LocalHost}/api/Category", Category);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Failed to update category.");
                return Page();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(short id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{Constant.LocalHost}/api/Category/{id}");

            if (!response.IsSuccessStatusCode)
            {
                TempData["ErrorMessage"] = "Failed to delete category.";
                return RedirectToPage();
            }

            return RedirectToPage();
        }
    }
}

