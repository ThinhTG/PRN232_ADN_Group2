using Repository.Entity;
using Repository.Repository;
using Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repo;
        public AddressService(IAddressRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Address>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Address> GetByIdAsync(object id) => await _repo.GetByIdAsync(id);
        public async Task<Address> AddAsync(Address entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            return entity;
        }
        public async Task<Address> UpdateAsync(Address entity)
        {
            _repo.Update(entity);
            await _repo.SaveAsync();
            return entity;
        }
        public async Task<bool> DeleteAsync(object id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;
            _repo.Delete(entity);
            await _repo.SaveAsync();
            return true;
        }
    }
} 