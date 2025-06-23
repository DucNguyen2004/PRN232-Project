using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace FUNewsSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController()
        {
            _newsService = new NewsService();
        }

        // GET: api/news
        [HttpGet]
        public ActionResult<List<NewsArticle>> GetFilterNews([FromQuery] short? categoryId)
        {
            var filteredNews = _newsService.GetAllNews();

            if (categoryId.HasValue)
                filteredNews = filteredNews.Where(n => n.CategoryId == categoryId.Value).ToList();
            return filteredNews;
        }

        // GET: api/news/{id}
        [HttpGet("{id}")]
        public ActionResult<NewsArticle> GetNewsById(string id)
        {
            var news = _newsService.GetNewsById(id);
            if (news == null) return NotFound();
            return Ok(news);
        }

        // GET: api/news/category/{categoryId}
        [HttpGet("category/{categoryId}")]
        public ActionResult<List<NewsArticle>> GetNewsByCategory(short categoryId)
        {
            return _newsService.GetNewsByCategory(categoryId);
        }

        // GET: api/news/creator/{userId}
        [HttpGet("creator/{userId}")]
        public ActionResult<List<NewsArticle>> GetNewsByCreator(short userId)
        {
            return _newsService.GetNewsByCreator(userId);
        }

        // POST: api/news
        [HttpPost]
        public IActionResult AddNews([FromBody] NewsArticle news)
        {
            _newsService.AddNews(news);
            return CreatedAtAction(nameof(GetNewsById), new { id = news.NewsArticleId }, news);
        }

        // PUT: api/news
        [HttpPut]
        public IActionResult UpdateNews([FromBody] NewsArticle news)
        {
            var existing = _newsService.GetNewsById(news.NewsArticleId);
            if (existing == null) return NotFound();
            _newsService.UpdateNews(news);
            return NoContent();
        }

        // DELETE: api/news/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteNews(string id)
        {
            var existing = _newsService.GetNewsById(id);
            if (existing == null) return NotFound();
            _newsService.DeleteNews(id);
            return NoContent();
        }
    }
}
