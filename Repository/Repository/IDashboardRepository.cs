using Repository.Entity;

namespace Repository.Repository
{
    public interface IDashboardRepository
    {
        Task<IEnumerable<Dictionary<int,decimal>>> GetMonthlyRevenueAsync(int year);
        Task<IEnumerable<Service>> OrderAsync();
        Task<int> TotalAccountAsync();
        Task<int> TotalAppointmentAsync();
        Task<decimal> TotalRevenueAsync();
    }

}
