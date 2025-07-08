using Repository.DBContext;
using Repository.Entity;

namespace Repository.Repository
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(ADNDbContext context) : base(context) { }
    }
}
