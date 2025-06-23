using BusinessObjects;
using Repositories;

namespace Services
{
    public class ReportService : IReportService
    {
        private readonly INewsRepository _newsRepository = new NewsRepository();
        public List<NewsArticle> GenerateReport(DateTime startDate, DateTime endDate)
        {
            return _newsRepository.GetNewsByDateRange(startDate, endDate);
        }
    }
}
