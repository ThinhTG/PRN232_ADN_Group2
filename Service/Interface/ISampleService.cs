using Service.DTOs;

namespace Service.Interface
{
    public interface ISampleService
    {
        Task<IEnumerable<SampleReadDTO>> GetAllAsync();
        Task<SampleReadDTO> GetByIdAsync(object id);
        Task<SampleReadDTO> AddAsync(SampleCreateUpdateDTO entity);
        Task<SampleReadDTO> UpdateAsync(Guid id,SampleCreateUpdateDTO entity);
        Task<bool> DeleteAsync(object id);
    }
} 