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
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _repo;
        public FeedbackService(IFeedbackRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<FeedbackReadDTO>> GetAllAsync()
        {
            var feedbacks = await _repo.GetAllAsync();
            return feedbacks.Select(f => new FeedbackReadDTO
            {
                FeedbackId = f.FeedbackId,
                UserId = f.UserId,
                Comment = f.Comment,
                Rating = f.Rating,
                ServiceId = f.ServiceId
            });
        }

        public async Task<FeedbackReadDTO> GetByIdAsync(Guid id)
        {
            var f = await _repo.GetByIdAsync(id);
            if (f == null) return null;
            return new FeedbackReadDTO
            {
                FeedbackId = f.FeedbackId,
                UserId = f.UserId,
                Comment = f.Comment,
                Rating = f.Rating,
                ServiceId = f.ServiceId
            };
        }

        public async Task<FeedbackReadDTO> AddAsync(FeedbackCreateUpdateDTO dto)
        {
            var feedback = new Feedback
            {
                FeedbackId = Guid.NewGuid(),
                UserId = dto.UserId,
                Comment = dto.Comment,
                Rating = dto.Rating,
                ServiceId = dto.ServiceId
            };

            await _repo.AddAsync(feedback);
            await _repo.SaveAsync();

            return new FeedbackReadDTO
            {
                FeedbackId = feedback.FeedbackId,
                UserId = feedback.UserId,
                Comment = feedback.Comment,
                Rating = feedback.Rating,
                ServiceId = feedback.ServiceId
            };
        }

        public async Task<bool> UpdateAsync(Guid id, FeedbackCreateUpdateDTO dto)
        {
            var feedback = await _repo.GetByIdAsync(id);
            if (feedback == null) return false;

            feedback.UserId = dto.UserId;
            feedback.Comment = dto.Comment;
            feedback.Rating = dto.Rating;
            feedback.ServiceId = dto.ServiceId;

            _repo.Update(feedback);
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