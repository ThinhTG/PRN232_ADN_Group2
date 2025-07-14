using Repository.Repository;
using Service.DTOs;
using Service.Interface;

namespace Service
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _repo;

        public DashboardService(IDashboardRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Revenue>> GetMonthlyRevenueAsync(int year)
        {
            var rawData = await _repo.GetMonthlyRevenueAsync(year);

            // Chuyển danh sách Dictionary<int, decimal> thành Dictionary<int, decimal>
            var revenueDict = rawData
                .SelectMany(d => d) // Vì mỗi phần tử là Dictionary<int, decimal>
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            // Tạo danh sách đủ 12 tháng
            var fullResult = Enumerable.Range(1, 12)
                .Select(month => new Revenue
                {
                    Month = month,
                    TotalRevenue = revenueDict.ContainsKey(month) ? revenueDict[month] : 0
                })
                .OrderBy(r => r.Month); // sắp xếp theo tháng (không bắt buộc)

            return fullResult;
        }

        public async Task<IEnumerable<ServiceReadDTO>> OrderServiceAsync()
        {
            var services = await _repo.OrderAsync();
            return services.Select(s => new ServiceReadDTO
            {
                ServiceId = s.ServiceId,
                Name = s.Name,
                Price = s.Price,
                AllowHomeKit = s.AllowHomeKit,
                Description = s.Description,
                Type = s.Type,
                Url = s.Url
            });
        }
        public Task<int> TotalAccount() => _repo.TotalAccountAsync();
        public Task<int> TotalAppointment() => _repo.TotalAppointmentAsync();
        public Task<decimal> TotalRevenue() => _repo.TotalRevenueAsync();
    }
}
