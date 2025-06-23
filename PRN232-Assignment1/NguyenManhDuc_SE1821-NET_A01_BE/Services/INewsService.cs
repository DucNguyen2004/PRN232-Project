using BusinessObjects;

namespace Services
{
    public interface INewsService
    {
        public List<NewsArticle> GetAllNews();
        public NewsArticle GetNewsById(string id);
        public void AddNews(NewsArticle news);
        public void UpdateNews(NewsArticle news);
        public void DeleteNews(string id);
        List<NewsArticle> GetNewsByCategory(short categoryId);
        List<NewsArticle> GetNewsByCreator(short userId);

    }
}
