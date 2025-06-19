using Repository.DBContext;
using Repository.Entity;

namespace Repository.Repository
{
    public class SampleRepository : GenericRepository<Sample>, ISampleRepository
    {
        public SampleRepository(ADNDbContext context) : base(context) { }
    }
} 