using Net.payOS.Types;
using Repository.Entity;
using Service.DTOs;

namespace Service.Interface
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentReadDTO>> GetAllAsync();
        Task<CreatePaymentResult> CreateLinkAsync(PaymentResponse request);
        Task CheckStatusAsync(string orderCode);
        Task<PaymentReadDTO> GetByIdAsync(object id);
        Task<bool> DeleteAsync(object id);
        Task<IEnumerable<PaymentReadDTO>> GetPaymentByUserIdAsync(Guid userId);
    }
} 