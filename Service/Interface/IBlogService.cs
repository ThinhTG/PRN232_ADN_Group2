using Service.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogReadDTO>> GetAllAsync();
        Task<BlogReadDTO> GetByIdAsync(int id);
        Task<BlogReadDTO> AddAsync(BlogCreateUpdateDTO entity);
        Task<bool> UpdateAsync(int id, BlogCreateUpdateDTO entity);
        Task<bool> DeleteAsync(int id);
    }
} 