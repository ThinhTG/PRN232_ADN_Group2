using Repository.Entity;
using Repository.Repository;
using Service.Interface;

namespace Service
{
    public class SampleService : ISampleService
    {
        private readonly ISampleRepository _repo;
        public SampleService(ISampleRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Sample>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Sample> GetByIdAsync(object id) => await _repo.GetByIdAsync(id);
        public async Task<Sample> AddAsync(Sample entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            return entity;
        }
        public async Task<Sample> UpdateAsync(Sample entity)
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