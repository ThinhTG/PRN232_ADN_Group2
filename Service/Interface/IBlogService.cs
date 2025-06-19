using Repository.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(object id);
        Task<Blog> AddAsync(Blog entity);
        Task<Blog> UpdateAsync(Blog entity);
        Task<bool> DeleteAsync(object id);
    }
} 