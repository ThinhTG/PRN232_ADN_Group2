using Repository.Entity;
using Repository.Repository;
using Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class KitService : IKitService
    {
        private readonly IKitRepository _repo;
        public KitService(IKitRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Kit>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Kit> GetByIdAsync(object id) => await _repo.GetByIdAsync(id);
        public async Task<Kit> AddAsync(Kit entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            return entity;
        }
        public async Task<Kit> UpdateAsync(Kit entity)
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