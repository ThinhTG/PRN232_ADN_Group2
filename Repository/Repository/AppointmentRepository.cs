using Core.enums;
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
        public async Task<IEnumerable<Appointment>> GetAllAppointment(bool? isHomeKit, AppointmentStatus? status)
        {
            IQueryable<Appointment> query = _context.Appointments
                .Include(x => x.Service);
            if (isHomeKit.HasValue && isHomeKit.Value)
            {
                query = query.Where(x => x.IsHomeKit == isHomeKit.Value);
            }
            else if (isHomeKit.HasValue && !isHomeKit.Value)
            {
                query = query.Where(x => x.IsHomeKit == isHomeKit.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(x => x.Status.Equals(status.ToString()));
            }
            return query;
        }
    }
} 