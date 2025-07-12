using Microsoft.EntityFrameworkCore;
using Repository.DBContext;
using Repository.Entity;

namespace Repository.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ADNDbContext context) : base(context) { }

        public async Task<IEnumerable<Appointment>> GetByUserIdAsync(Guid userId)
        {
            return  _context.Appointments
                .Where(p => p.UserId == userId);
        }
    }
} 