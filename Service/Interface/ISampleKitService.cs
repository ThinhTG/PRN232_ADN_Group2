using Repository.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ISampleKitService
    {
        Task<IEnumerable<SampleKit>> GetAllAsync();
        Task<SampleKit> GetByIdAsync(object id);
        Task<SampleKit> AddAsync(SampleKit entity);
        Task<SampleKit> UpdateAsync(SampleKit entity);
        Task<bool> DeleteAsync(object id);
    }
} 