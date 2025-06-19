using Repository.DBContext;
using Repository.Entity;

namespace Repository.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(ADNDbContext context) : base(context) { }
    }
} 