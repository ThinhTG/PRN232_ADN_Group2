using Repository.Entity;

namespace Repository.Repository
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Appointment>> GetByStatusAsync(string status);
    }
} 