using Service.DTOs;

namespace Service.Interface
{
    public interface IDashboardService
    {
        Task<IEnumerable<Revenue>> GetMonthlyRevenueAsync(int year);
        Task<IEnumerable<ServiceReadDTO>> OrderServiceAsync();
        Task<int> TotalAccount();
        Task<int> TotalAppointment();
        Task<Decimal> TotalRevenue();
    }
}
