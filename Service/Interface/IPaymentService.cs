using Net.payOS.Types;
using Repository.Entity;
using Service.DTOs;

namespace Service.Interface
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAllAsync();
        Task<CreatePaymentResult> CreateLinkAsync(PaymentResponse request);
        Task CheckStatusAsync(string orderCode);
        Task<Payment> GetByIdAsync(object id);
        Task<Payment> AddAsync(Payment entity);
        Task<Payment> UpdateAsync(Payment entity);
        Task<bool> DeleteAsync(object id);
    }
} 