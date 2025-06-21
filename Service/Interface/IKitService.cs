using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IKitService
    {
        Task<IEnumerable<KitReadDTO>> GetAllAsync();
        Task<KitReadDTO> GetByIdAsync(Guid id);
        Task<KitReadDTO> AddAsync(KitCreateUpdateDTO entity);
        Task<bool> UpdateAsync(Guid id, KitCreateUpdateDTO entity);
        Task<bool> DeleteAsync(Guid id);
    }
} 