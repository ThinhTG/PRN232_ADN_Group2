using Microsoft.EntityFrameworkCore;
using Repository.DBContext;
using Repository.Entity;

namespace Repository.Repository
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ADNDbContext context) : base(context) { }

        public async Task<Payment?> GetByOrderCodeAsync(int orderCode)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(p => p.OrderCode == orderCode);
        }
        public async Task<IEnumerable<Payment>> GetByUserIdAsync(Guid userId)
        {
            return _context.Payments.
                Include(p => p.Appointment)
                .Where(p => p.Appointment.UserId == userId);
        }
    }
} 