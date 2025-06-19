using Repository.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ISampleService
    {
        Task<IEnumerable<Sample>> GetAllAsync();
        Task<Sample> GetByIdAsync(object id);
        Task<Sample> AddAsync(Sample entity);
        Task<Sample> UpdateAsync(Sample entity);
        Task<bool> DeleteAsync(object id);
    }
} 