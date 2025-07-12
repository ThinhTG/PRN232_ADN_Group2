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
        /// Chỉ cho phép feedback khi appointment đã hoàn thành, đúng user, đúng service, và chưa feedback cho appointment đó
        Task<FeedbackReadDTO> AddAsync(FeedbackCreateUpdateDTO entity);
        Task<bool> UpdateAsync(Guid id, FeedbackCreateUpdateDTO entity);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<FeedbackReadDTO>> GetByServiceIdAsync(Guid serviceId);
        Task<IEnumerable<FeedbackReadDTO>> GetByUserIdAsync(Guid userId);
    }
} 