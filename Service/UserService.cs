using ADN_Group2.BusinessObject.Identity;
using Core.Utils;
using Repository.Entity;
using Repository.Repository;
using Service.DTOs;
using Service.Interface;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repo;

        public UserService(UserRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<UserReadDTO>> GetAllAsync()
        {
            var users = await _repo.GetAllAsync();
            return users.Select(MapToDTO);
        }

        public async Task<UserReadDTO?> GetByIdAsync(Guid id)
        {
            var user = await _repo.FindByIdAsync(id);
            return user == null ? null : MapToDTO(user);
        }

        public async Task<bool> UpdateAsync(Guid id, UserCreateUpdateDTO request)
        {
            var user = await _repo.FindByIdAsync(id);
            if (user == null) return false;

            user.FullName = request.FullName ?? user.FullName;
            user.AvatarUrl = request.AvatarUrl ?? user.AvatarUrl;
            user.Gender = request.Gender ?? user.Gender;
            user.LastUpdatedTime = CoreHelper.SystemTimeNow;

            await _repo.UpdateUserAsync(user);
            return true;
        }

        private UserReadDTO MapToDTO(ApplicationUser user)
        {
            return new UserReadDTO
            {
                Id = user.Id,
                FullName = user.FullName,
                AvatarUrl = user.AvatarUrl,
                Gender = user.Gender,
                Email = user.Email,
                Role = user.UserRoles.FirstOrDefault()?.Role?.Name,
                Addresses = GetUserAddressAsync(user.Addresses).ToList() ?? new List<AddressReadDTO>()
            };
        }
        public IEnumerable<AddressReadDTO> GetUserAddressAsync(ICollection<Address> addresses)
        {
            return addresses.Select(a => new AddressReadDTO
            {
                AddressId = a.AddressId,
                Number = a.Number,
                District = a.District,
                Province = a.Province,
                UserId = a.UserId
            });
        }
    }
}
