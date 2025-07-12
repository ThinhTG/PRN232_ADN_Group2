using Repository.Entity;

namespace Repository.Repository
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
        Task<Payment?> GetByOrderCodeAsync(int orderCode);
        Task<IEnumerable<Payment>> GetByUserIdAsync(Guid userId);
    }
} 