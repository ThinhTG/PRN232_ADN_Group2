using Repository.Entity;
using Repository.Repository;
using Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class SampleKitService : ISampleKitService
    {
        private readonly ISampleKitRepository _repo;
        public SampleKitService(ISampleKitRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<SampleKit>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<SampleKit> GetByIdAsync(object id) => await _repo.GetByIdAsync(id);
        public async Task<SampleKit> AddAsync(SampleKit entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            return entity;
        }
        public async Task<SampleKit> UpdateAsync(SampleKit entity)
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