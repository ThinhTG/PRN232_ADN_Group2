using Repository.Entity;
using Repository.Repository;
using Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _repo;
        public FeedbackService(IFeedbackRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Feedback>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Feedback> GetByIdAsync(object id) => await _repo.GetByIdAsync(id);
        public async Task<Feedback> AddAsync(Feedback entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            return entity;
        }
        public async Task<Feedback> UpdateAsync(Feedback entity)
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