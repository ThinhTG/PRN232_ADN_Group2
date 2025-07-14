using Service.DTOs;

namespace Service.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDTO>> GetAllAsync();
        Task<UserReadDTO?> GetByIdAsync(Guid id);
        Task<bool> UpdateAsync(Guid id, UserCreateUpdateDTO request);
    }
    
}
