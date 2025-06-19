using Repository.DBContext;
using Repository.Entity;

namespace Repository.Repository
{
    public class SampleKitRepository : GenericRepository<SampleKit>, ISampleKitRepository
    {
        public SampleKitRepository(ADNDbContext context) : base(context) { }
    }
} 