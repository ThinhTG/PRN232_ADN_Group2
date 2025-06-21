using Repository.Entity;
using Repository.Repository;
using Service.DTOs;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<AddressReadDTO>> GetAllAsync()
        {
            var addresses = await _repo.GetAllAsync();
            return addresses.Select(a => new AddressReadDTO
            {
                AddressId = a.AddressId,
                Number = a.Number,
                District = a.District,
                Province = a.Province,
                UserId = a.UserId
            });
        }

        public async Task<AddressReadDTO> GetByIdAsync(Guid id)
        {
            var address = await _repo.GetByIdAsync(id);
            if (address == null) return null;

            return new AddressReadDTO
            {
                AddressId = address.AddressId,
                Number = address.Number,
                District = address.District,
                Province = address.Province,
                UserId = address.UserId
            };
        }

        public async Task<AddressReadDTO> AddAsync(AddressCreateUpdateDTO dto)
        {
            var address = new Address
            {
                AddressId = Guid.NewGuid(),
                Number = dto.Number,
                District = dto.District,
                Province = dto.Province,
                UserId = dto.UserId
            };

            await _repo.AddAsync(address);
            await _repo.SaveAsync();

            return new AddressReadDTO
            {
                AddressId = address.AddressId,
                Number = address.Number,
                District = address.District,
                Province = address.Province,
                UserId = address.UserId
            };
        }

        public async Task<bool> UpdateAsync(Guid id, AddressCreateUpdateDTO dto)
        {
            var address = await _repo.GetByIdAsync(id);
            if (address == null) return false;

            address.Number = dto.Number;
            address.District = dto.District;
            address.Province = dto.Province;
            address.UserId = dto.UserId;

            _repo.Update(address);
            await _repo.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            _repo.Delete(entity);
            await _repo.SaveAsync();
            return true;
        }
    }
} 