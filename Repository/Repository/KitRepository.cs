using Repository.DBContext;
using Repository.Entity;

namespace Repository.Repository
{
    public class KitRepository : GenericRepository<Kit>, IKitRepository
    {
        public KitRepository(ADNDbContext context) : base(context) { }
    }
} 