using Repository.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IKitService
    {
        Task<IEnumerable<Kit>> GetAllAsync();
        Task<Kit> GetByIdAsync(object id);
        Task<Kit> AddAsync(Kit entity);
        Task<Kit> UpdateAsync(Kit entity);
        Task<bool> DeleteAsync(object id);
    }
} 