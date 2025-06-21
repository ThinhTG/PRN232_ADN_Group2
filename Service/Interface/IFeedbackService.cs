using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IFeedbackService
    {
        Task<IEnumerable<FeedbackReadDTO>> GetAllAsync();
        Task<FeedbackReadDTO> GetByIdAsync(Guid id);
        Task<FeedbackReadDTO> AddAsync(FeedbackCreateUpdateDTO entity);
        Task<bool> UpdateAsync(Guid id, FeedbackCreateUpdateDTO entity);
        Task<bool> DeleteAsync(Guid id);
    }
} 