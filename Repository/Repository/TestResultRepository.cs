using Repository.DBContext;
using Repository.Entity;

namespace Repository.Repository
{
    public class TestResultRepository : GenericRepository<TestResult>, ITestResultRepository
    {
        public TestResultRepository(ADNDbContext context) : base(context) { }
    }
} 