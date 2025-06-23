using BusinessObjects;

namespace Services
{
    public interface IReportService
    {
        List<NewsArticle> GenerateReport(DateTime startDate, DateTime endDate);
    }
}
