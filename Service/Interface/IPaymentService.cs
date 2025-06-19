using Repository.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<Payment> GetByIdAsync(object id);
        Task<Payment> AddAsync(Payment entity);
        Task<Payment> UpdateAsync(Payment entity);
        Task<bool> DeleteAsync(object id);
    }
} 