using Repository.Entity;
using Repository.Repository;
using Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repo;
        public PaymentService(IPaymentRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Payment> GetByIdAsync(object id) => await _repo.GetByIdAsync(id);
        public async Task<Payment> AddAsync(Payment entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            return entity;
        }
        public async Task<Payment> UpdateAsync(Payment entity)
        {
            _repo.Update(entity);
            await _repo.SaveAsync();
            return entity;
        }
        public async Task<bool> DeleteAsync(object id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;
            _repo.Delete(entity);
            await _repo.SaveAsync();
            return true;
        }
    }
} 