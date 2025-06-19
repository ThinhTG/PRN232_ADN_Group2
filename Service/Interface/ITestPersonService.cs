using Repository.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ITestPersonService
    {
        Task<IEnumerable<TestPerson>> GetAllAsync();
        Task<TestPerson> GetByIdAsync(object id);
        Task<TestPerson> AddAsync(TestPerson entity);
        Task<TestPerson> UpdateAsync(TestPerson entity);
        Task<bool> DeleteAsync(object id);
    }
} 