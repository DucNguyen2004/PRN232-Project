using BusinessObjects;
using Repositories;

namespace Services
{
    public class NewsService : INewsService
    {
        private readonly INewsRepository newsRepository = new NewsRepository();

        public List<NewsArticle> GetAllNews() => newsRepository.GetAllNews();
        public NewsArticle GetNewsById(string id) => newsRepository.GetNewsById(id);
        public void AddNews(NewsArticle news) => newsRepository.AddNews(news);
        public void UpdateNews(NewsArticle news) => newsRepository.UpdateNews(news);
        public void DeleteNews(string id) => newsRepository.DeleteNews(id);

        public List<NewsArticle> GetNewsByCategory(short categoryId)
        {
            return newsRepository.GetNewsByCategory(categoryId);
        }

        public List<NewsArticle> GetNewsByCreator(short userId)
        {
            return newsRepository.GetNewsByCreator(userId);
        }
    }
}
