using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAddressService
    {
        Task<IEnumerable<AddressReadDTO>> GetAllAsync();
        Task<AddressReadDTO> GetByIdAsync(Guid id);
        Task<AddressReadDTO> AddAsync(AddressCreateUpdateDTO entity);
        Task<bool> UpdateAsync(Guid id, AddressCreateUpdateDTO entity);
        Task<bool> DeleteAsync(Guid id);
    }
} 