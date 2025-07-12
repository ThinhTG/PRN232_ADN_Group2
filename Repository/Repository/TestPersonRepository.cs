using Microsoft.EntityFrameworkCore;
using Repository.DBContext;
using Repository.Entity;

namespace Repository.Repository
{
    public class TestPersonRepository : GenericRepository<TestPerson>, ITestPersonRepository
    {
        public TestPersonRepository(ADNDbContext context) : base(context) { }

        public async Task<List<Guid>> GetTestPersonIdByAppointment(Guid appointmentId)
        {
            return _context.TestPersons
                .Where(p => p.AppointmentId == appointmentId)
                .Select(x => x.PersonId).ToList();

        }
    }
} 