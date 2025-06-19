using Repository.DBContext;
using Repository.Entity;

namespace Repository.Repository
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ADNDbContext context) : base(context) { }
    }
} 