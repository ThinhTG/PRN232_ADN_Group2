using Core.enums;
using Microsoft.EntityFrameworkCore;
using Repository.DBContext;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ADNDbContext _context;

        public DashboardRepository(ADNDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Dictionary<int, decimal>>> GetMonthlyRevenueAsync(int year)
        {
            var result = await _context.Payments
                .Include(p => p.Appointment)
                .Where(p => p.PaidDate.HasValue &&
                            p.PaidDate.Value.Year == year &&
                            p.Appointment.Status != AppointmentStatus.Pending.ToString())
                .GroupBy(p => p.PaidDate.Value.Month)
                .Select(g => new
                {
                    Month = g.Key,
                    Total = g.Sum(p => p.Amount)
                })
                .ToListAsync();

            // Trả về kiểu Dictionary như yêu cầu
            return result.Select(r => new Dictionary<int, decimal> { { r.Month, r.Total } }).ToList();
        }

        public async Task<IEnumerable<Service>> OrderAsync()
        {
            return await _context.Appointments
                .Include(a => a.Service)
                .Where(a =>a.Status != AppointmentStatus.Pending.ToString())
                .GroupBy(a => a.Service)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .ToListAsync();
        }

        public Task<int> TotalAccountAsync()
        {
            return _context.Users.CountAsync();
        }

        public Task<int> TotalAppointmentAsync()
        {
            return _context.Appointments
                .Where(a => a.Status != AppointmentStatus.Pending.ToString())
                .CountAsync();
        }

        public Task<decimal> TotalRevenueAsync()
        {
            return _context.Payments
                .Include(p => p.Appointment)
                .Where(p => p.PaidDate.HasValue && p.Appointment.Status != AppointmentStatus.Pending.ToString())
                .SumAsync(p => p.Amount);
        }
    }
}
