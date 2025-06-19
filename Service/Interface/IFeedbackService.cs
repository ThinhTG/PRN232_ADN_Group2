using Repository.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllAsync();
        Task<Feedback> GetByIdAsync(object id);
        Task<Feedback> AddAsync(Feedback entity);
        Task<Feedback> UpdateAsync(Feedback entity);
        Task<bool> DeleteAsync(object id);
    }
} 