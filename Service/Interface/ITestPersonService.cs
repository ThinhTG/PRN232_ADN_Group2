using Service.DTOs;
namespace Service.Interface
{
    public interface ITestPersonService
    {
        Task<IEnumerable<TestPersonReadDTO>> GetAllAsync();
        Task<TestPersonReadDTO> GetByIdAsync(object id);
        Task<List<TestPersonReadDTO>> AddAsync(List<TestPersonCreateUpdateDTO> entity);
        Task<TestPersonReadDTO> UpdateAsync(Guid id,TestPersonCreateUpdateDTO entity);
        Task<bool> DeleteAsync(object id);
    }
} 