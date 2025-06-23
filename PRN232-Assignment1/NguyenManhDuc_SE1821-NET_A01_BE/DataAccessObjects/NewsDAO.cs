using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessObjects
{
    public class NewsDAO
    {
        public List<NewsArticle> GetAllNews()
        {
            using (var context = new FunewsManagementContext())
            {
                return context.NewsArticles
                    .Include(n => n.Category)
                    .Where(n => n.NewsStatus.HasValue && n.NewsStatus.Value)
                    .Include(n => n.CreatedBy)
                    .ToList();
            }
        }

        public NewsArticle GetNewsById(string id)
        {
            using (var context = new FunewsManagementContext())
            {
                return context.NewsArticles
                    .Include(n => n.Category)
                    .FirstOrDefault(n => n.NewsArticleId == id);
            }
        }

        public void AddNews(NewsArticle news)
        {
            using (var context = new FunewsManagementContext())
            {
                news.NewsArticleId = (context.NewsArticles.Count() + 1).ToString();
                context.NewsArticles.Add(news);
                context.SaveChanges();
            }
        }

        public void UpdateNews(NewsArticle news)
        {
            using (var context = new FunewsManagementContext())
            {
                var existingNews = context.NewsArticles.Find(news.NewsArticleId);
                if (existingNews != null)
                {
                    existingNews.NewsTitle = news.NewsTitle;
                    existingNews.Headline = news.Headline;
                    existingNews.CategoryId = news.CategoryId;
                    existingNews.NewsContent = news.NewsContent;
                    existingNews.ModifiedDate = DateTime.Now;
                    context.SaveChanges();
                }
            }
        }

        public void DeleteNews(string id)
        {
            using (var context = new FunewsManagementContext())
            {
                var article = context.NewsArticles
                    .Include(n => n.Tags)
                    .FirstOrDefault(n => n.NewsArticleId.Equals(id));

                if (article.Tags != null && article.Tags.Any())
                {
                    article.Tags.Clear();
                }

                context.NewsArticles.Remove(article);
                context.SaveChanges();
            }
        }
        public List<NewsArticle> GetNewsByDateRange(DateTime startDate, DateTime endDate)
        {
            using (var context = new FunewsManagementContext())
            {
                return context.NewsArticles
                                .Include(n => n.Category)
                                .Where(n => n.CreatedDate >= startDate && n.CreatedDate < endDate.Date.AddDays(1))
                                .OrderByDescending(n => n.CreatedDate)
                                .ToList();
            }

        }
        public List<NewsArticle> GetNewsByCategory(short categoryId)
        {
            using (var context = new FunewsManagementContext())
            {
                return context.NewsArticles
                                .Include(n => n.Category)
                                .Where(n => n.Category.CategoryId == categoryId)
                                .ToList();
            }
        }

        public List<NewsArticle> GetNewsByCreator(short userId)
        {
            using var context = new FunewsManagementContext();
            return context.NewsArticles
                        .Include(n => n.Category)
                          .Where(n => n.CreatedById == userId)
                          .OrderByDescending(n => n.CreatedDate)
                          .ToList();
        }
    }
}
