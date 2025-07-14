using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs;
using Service.Interface;

namespace ADN_Group2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("monthly-revenue")]
        public async Task<ActionResult<IEnumerable<Revenue>>> GetMonthlyRevenue([FromQuery] int year)
        {
            var result = await _dashboardService.GetMonthlyRevenueAsync(year);
            return Ok(result);
        }

        [HttpGet("top-services")]
        public async Task<ActionResult<IEnumerable<ServiceReadDTO>>> GetTopOrderedServices()
        {
            var result = await _dashboardService.OrderServiceAsync();
            return Ok(result);
        }

        [HttpGet("total-accounts")]
        public async Task<ActionResult<int>> GetTotalAccounts()
        {
            var count = await _dashboardService.TotalAccount();
            return Ok(count);
        }

        [HttpGet("total-appointments")]
        public async Task<ActionResult<int>> GetTotalAppointments()
        {
            var count = await _dashboardService.TotalAppointment();
            return Ok(count);
        }

        [HttpGet("total-revenue")]
        public async Task<ActionResult<decimal>> GetTotalRevenue()
        {
            var total = await _dashboardService.TotalRevenue();
            return Ok(total);
        }
    }
}
