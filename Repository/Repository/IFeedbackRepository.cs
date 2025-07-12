using Repository.Entity;

namespace Repository.Repository
{
    public interface IFeedbackRepository : IGenericRepository<Feedback>
    {
        Task<Feedback?> GetByUserIdIncludeAsync(Guid userId);
        Task<IEnumerable<Feedback>> GetAllFeedbackAsync();
    }
} 