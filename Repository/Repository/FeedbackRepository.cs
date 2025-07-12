using Microsoft.EntityFrameworkCore;
using Repository.DBContext;
using Repository.Entity;

namespace Repository.Repository
{
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        public FeedbackRepository(ADNDbContext context) : base(context) { }

        public async Task<IEnumerable<Feedback>> GetAllFeedbackAsync()
        {
            return  _context.Feedbacks
                .Include(x => x.Appointment)
                .ThenInclude(x => x.Service);
        }

        public async Task<Feedback?> GetByUserIdIncludeAsync(Guid userId)
        {
            return await _context.Feedbacks
                .Include(x => x.Appointment)
                .ThenInclude(x => x.Service)
                .FirstOrDefaultAsync(x => x.Appointment.UserId == userId);
        }
    }
} 