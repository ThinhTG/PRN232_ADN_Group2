using Repository.Entity;
using Repository.Repository;
using Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class TestResultService : ITestResultService
    {
        private readonly ITestResultRepository _repo;
        public TestResultService(ITestResultRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TestResult>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<TestResult> GetByIdAsync(object id) => await _repo.GetByIdAsync(id);
        public async Task<TestResult> AddAsync(TestResult entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            return entity;
        }
        public async Task<TestResult> UpdateAsync(TestResult entity)
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