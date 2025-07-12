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
        private readonly IAppointmentRepository _appointmentRepo; // Added this line
        public FeedbackService(IFeedbackRepository repo, IAppointmentRepository appointmentRepo) // Modified constructor
        {
            _repo = repo;
            _appointmentRepo = appointmentRepo; // Initialize new field
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
            // Kiểm tra điều kiện feedback
            var appointment = await _appointmentRepo.GetByIdAsync(dto.AppointmentId);
            if (appointment == null || appointment.UserId != dto.UserId ||  appointment.Status != "Completed")
            {
                throw new InvalidOperationException("Bạn chỉ có thể feedback sau khi hoàn thành dịch vụ này.");
            }
            // Kiểm tra trùng lặp feedback cho appointment này
            var existingFeedbacks = await _repo.GetAllAsync();
            if (existingFeedbacks.Any(f => f.AppointmentId == dto.AppointmentId && f.UserId == dto.UserId))
            {
                throw new InvalidOperationException("Bạn đã feedback cho lần đặt lịch này rồi.");
            }
            var feedback = new Feedback
            {
                FeedbackId = Guid.NewGuid(),
                UserId = dto.UserId,
                Comment = dto.Comment,
                Rating = dto.Rating,
                AppointmentId = dto.AppointmentId,
                ServiceId = appointment.ServiceId // Lấy từ appointment
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
            feedback.AppointmentId = dto.AppointmentId;

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