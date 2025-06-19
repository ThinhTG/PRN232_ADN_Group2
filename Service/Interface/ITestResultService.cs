using Repository.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ITestResultService
    {
        Task<IEnumerable<TestResult>> GetAllAsync();
        Task<TestResult> GetByIdAsync(object id);
        Task<TestResult> AddAsync(TestResult entity);
        Task<TestResult> UpdateAsync(TestResult entity);
        Task<bool> DeleteAsync(object id);
    }
} 