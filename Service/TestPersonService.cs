using Repository.Entity;
using Repository.Repository;
using Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class TestPersonService : ITestPersonService
    {
        private readonly ITestPersonRepository _repo;
        public TestPersonService(ITestPersonRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TestPerson>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<TestPerson> GetByIdAsync(object id) => await _repo.GetByIdAsync(id);
        public async Task<TestPerson> AddAsync(TestPerson entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            return entity;
        }
        public async Task<TestPerson> UpdateAsync(TestPerson entity)
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