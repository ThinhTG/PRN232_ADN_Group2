using Repository.Entity;
using Repository.Repository;
using Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _repo;
        public BlogService(IBlogRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Blog>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Blog> GetByIdAsync(object id) => await _repo.GetByIdAsync(id);
        public async Task<Blog> AddAsync(Blog entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            return entity;
        }
        public async Task<Blog> UpdateAsync(Blog entity)
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