using Repository.Entity;
using Service.DTOs;

namespace Service.Interface
{
    public interface ITestResultService
    {
        Task<IEnumerable<TestResultReadDTO>> GetAllAsync();
        Task<TestResultReadDTO> GetByIdAsync(object id);
        Task<TestResultReadDTO> AddAsync(TestResultCreateUpdateDTO entity);
        Task<TestResultReadDTO> UpdateAsync(int id,TestResultCreateUpdateDTO entity);
        Task<bool> DeleteAsync(object id);
    }
} 