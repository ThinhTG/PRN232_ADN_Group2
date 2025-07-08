using Service.DTOs;

namespace Service.Interface
{
    public interface IServiceService
    {
        Task<IEnumerable<ServiceReadDTO>> GetAllAsync();
        Task<ServiceReadDTO> GetByIdAsync(Guid serviceId);
        Task<ServiceReadDTO> AddAsync(ServiceCreateUpdateDTO dto);
    }
}
