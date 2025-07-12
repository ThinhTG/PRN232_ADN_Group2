using Microsoft.EntityFrameworkCore;
using Repository.DBContext;
using Repository.Entity;

namespace Repository.Repository
{
    public class SampleRepository : GenericRepository<Sample>, ISampleRepository
    {
        public SampleRepository(ADNDbContext context) : base(context) { }

        public async Task<List<Sample>> GetByIdsAsync(List<Guid> ids)
        {
            return await _dbSet.Where(s => ids.Contains(s.SampleId)).ToListAsync();
        }
        public async Task<IEnumerable<Sample>> GetByAppointmentIdAsync(Guid appointmentId)
        {
            return  _context.Samples
                .Include(x=> x.Kit)
                .ThenInclude(x=> x.Appointment).
                Where(s => s.Kit.AppointmentId == appointmentId);
        }
    }
} 