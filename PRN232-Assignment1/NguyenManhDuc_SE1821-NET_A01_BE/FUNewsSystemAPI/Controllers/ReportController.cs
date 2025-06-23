using Microsoft.AspNetCore.Mvc;
using Services;

namespace FUNewsSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController()
        {
            _reportService = new ReportService();
        }

        [HttpGet]
        public IActionResult GetReport([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (startDate > endDate)
            {
                return BadRequest("Start date must be before end date.");
            }

            var report = _reportService.GenerateReport(startDate, endDate);
            return Ok(report);
        }
    }
}
