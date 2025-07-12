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
            // Validate rating
            if (dto.Rating < 1 || dto.Rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5");
            }

            // Check if appointment exists
            var appointment = await _appointmentRepo.GetByIdAsync(dto.AppointmentId);
            if (appointment == null)
            {
                throw new InvalidOperationException("Appointment not found with this ID.");
            }

            // Check if user has permission to feedback for this appointment
            if (appointment.UserId != dto.UserId)
            {
                throw new InvalidOperationException("You can only provide feedback for your own appointments.");
            }

            // Check if appointment is completed
            if (appointment.Status != "Completed")
            {
                throw new InvalidOperationException("You can only provide feedback after completing this service.");
            }

            // Check for duplicate feedback for this appointment
            var existingFeedbacks = await _repo.GetAllAsync();
            if (existingFeedbacks.Any(f => f.AppointmentId == dto.AppointmentId && f.UserId == dto.UserId))
            {
                throw new InvalidOperationException("You have already provided feedback for this appointment.");
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
                ServiceId = feedback.ServiceId,
                AppointmentId = feedback.AppointmentId
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

        public async Task<IEnumerable<FeedbackReadDTO>> GetByServiceIdAsync(Guid serviceId)
        {
            var feedbacks = await _repo.GetAllAsync();
            return feedbacks.Where(f => f.ServiceId == serviceId)
                           .Select(f => new FeedbackReadDTO
                           {
                               FeedbackId = f.FeedbackId,
                               UserId = f.UserId,
                               Comment = f.Comment,
                               Rating = f.Rating,
                               ServiceId = f.ServiceId,
                               AppointmentId = f.AppointmentId
                           });
        }

        public async Task<IEnumerable<FeedbackReadDTO>> GetByUserIdAsync(Guid userId)
        {
            var feedbacks = await _repo.GetAllAsync();
            return feedbacks.Where(f => f.UserId == userId)
                           .Select(f => new FeedbackReadDTO
                           {
                               FeedbackId = f.FeedbackId,
                               UserId = f.UserId,
                               Comment = f.Comment,
                               Rating = f.Rating,
                               ServiceId = f.ServiceId,
                               AppointmentId = f.AppointmentId
                           });
        }
    }
} 