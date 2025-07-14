using Microsoft.EntityFrameworkCore;
using Repository.DBContext;
using Repository.Entity;

namespace Repository.Repository
{
    public class TestResultRepository : GenericRepository<TestResult>, ITestResultRepository
    {
        public TestResultRepository(ADNDbContext context) : base(context) { }

        public async Task<IEnumerable<TestResult>> GetByAppointmentIdAsync(Guid appointmentId)
        {
            return await _context.TestResults
                .Where(tr => tr.AppointmentId == appointmentId)
                .ToListAsync();
        }
    }
} 