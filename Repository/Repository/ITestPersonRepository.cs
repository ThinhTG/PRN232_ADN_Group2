using Repository.Entity;

namespace Repository.Repository
{
    public interface ITestPersonRepository : IGenericRepository<TestPerson>
    {
        Task<List<Guid>> GetTestPersonIdByAppointment(Guid appointmentId);
    }
} 