using Repository.DBContext;
using Repository.Entity;

namespace Repository.Repository
{
    public class TestPersonRepository : GenericRepository<TestPerson>, ITestPersonRepository
    {
        public TestPersonRepository(ADNDbContext context) : base(context) { }
    }
} 