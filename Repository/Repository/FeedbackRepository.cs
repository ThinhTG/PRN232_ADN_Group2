using Repository.DBContext;
using Repository.Entity;

namespace Repository.Repository
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(ADNDbContext context) : base(context) { }
    }
} 