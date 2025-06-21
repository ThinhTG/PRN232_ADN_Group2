using Repository.DBContext;
using Repository.Entity;

namespace Repository.Repository
{
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        public BlogRepository(ADNDbContext context) : base(context)
        {
        }
    }
} 