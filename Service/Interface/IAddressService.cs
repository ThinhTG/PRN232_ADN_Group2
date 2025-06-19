using Repository.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAddressService
    {
        Task<IEnumerable<Address>> GetAllAsync();
        Task<Address> GetByIdAsync(object id);
        Task<Address> AddAsync(Address entity);
        Task<Address> UpdateAsync(Address entity);
        Task<bool> DeleteAsync(object id);
    }
} 