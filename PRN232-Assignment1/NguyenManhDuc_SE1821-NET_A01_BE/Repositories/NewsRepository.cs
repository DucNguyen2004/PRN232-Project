using BusinessObjects;
using DataAccessObjects;

namespace Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly NewsDAO _newsDAO = new NewsDAO();

        public List<NewsArticle> GetAllNews() => _newsDAO.GetAllNews();
        public NewsArticle GetNewsById(string id) => _newsDAO.GetNewsById(id);
        public void AddNews(NewsArticle news) => _newsDAO.AddNews(news);
        public void UpdateNews(NewsArticle news) => _newsDAO.UpdateNews(news);
        public void DeleteNews(string id) => _newsDAO.DeleteNews(id);

        public List<NewsArticle> GetNewsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _newsDAO.GetNewsByDateRange(startDate, endDate);
        }

        public List<NewsArticle> GetNewsByCategory(short categoryId)
        {
            return _newsDAO.GetNewsByCategory(categoryId);
        }

        public List<NewsArticle> GetNewsByCreator(short userId)
        {
            return _newsDAO.GetNewsByCreator(userId);
        }
    }
}
