using Repository.Entity;

namespace Repository.Repository
{
    public interface ITestResultRepository : IGenericRepository<TestResult>
    {
        Task<IEnumerable<TestResult>> GetByAppointmentIdAsync(Guid appointmentId);
    }
} 