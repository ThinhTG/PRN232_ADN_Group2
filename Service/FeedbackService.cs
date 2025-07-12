using Repository.Entity;
using Repository.Repository;
using Service.DTOs;
using Service.Interface;

namespace Service
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IAppointmentRepository _appointmentRepo; // Added this line
        public FeedbackService(IFeedbackRepository repo, IAppointmentRepository appointmentRepo) // Modified constructor
        {
            _feedbackRepository = repo;
            _appointmentRepo = appointmentRepo; // Initialize new field
        }

        public async Task<IEnumerable<FeedbackReadDTO>> GetAllAsync()
        {
            var feedbacks = await _feedbackRepository.GetAllFeedbackAsync();
            return feedbacks.Select(f => new FeedbackReadDTO
            {
                FeedbackId = f.FeedbackId,
                UserId = f.Appointment.UserId,
                Comment = f.Comment,
                Rating = f.Rating,
                ServiceId = f.Appointment.ServiceId,
                AppointmentId = f.AppointmentId
            });
        }

        public async Task<FeedbackReadDTO> GetByIdAsync(Guid id)
        {
            var f = await _feedbackRepository.GetByUserIdIncludeAsync(id);
            if (f == null) return null;
            return new FeedbackReadDTO
            {
                FeedbackId = f.FeedbackId,
                UserId = f.Appointment.UserId,
                Comment = f.Comment,
                Rating = f.Rating,
                ServiceId = f.Appointment.ServiceId,
                AppointmentId = f.AppointmentId
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
            var existingFeedbacks = await _feedbackRepository.GetAllFeedbackAsync();
            if (existingFeedbacks.Any(f => f.AppointmentId == dto.AppointmentId && f.Appointment.UserId == dto.UserId))
            {
                throw new InvalidOperationException("You have already provided feedback for this appointment.");
            }

            var feedback = new Feedback
            {
                FeedbackId = Guid.NewGuid(),
                Comment = dto.Comment,
                Rating = dto.Rating,
                AppointmentId = dto.AppointmentId,
            };

            await _feedbackRepository.AddAsync(feedback);
            await _feedbackRepository.SaveAsync();

            return new FeedbackReadDTO
            {
                FeedbackId = feedback.FeedbackId,
                UserId = feedback.Appointment.UserId,
                Comment = feedback.Comment,
                Rating = feedback.Rating,
                ServiceId = feedback.Appointment.ServiceId,
                AppointmentId = feedback.AppointmentId
            };
        }

        public async Task<bool> UpdateAsync(Guid id, FeedbackCreateUpdateDTO dto)
        {
            var feedback = await _feedbackRepository.GetByUserIdIncludeAsync(id);
            if (feedback == null) return false;

            feedback.Comment = dto.Comment;
            feedback.Rating = dto.Rating;
            feedback.AppointmentId = dto.AppointmentId;

            _feedbackRepository.Update(feedback);
            await _feedbackRepository.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _feedbackRepository.GetByIdAsync(id);
            if (entity == null) return false;
            _feedbackRepository.Delete(entity);
            await _feedbackRepository.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<FeedbackReadDTO>> GetByServiceIdAsync(Guid serviceId)
        {
            var feedbacks = await _feedbackRepository.GetAllFeedbackAsync();
            return feedbacks.Where(f => f.Appointment.ServiceId == serviceId)
                           .Select(f => new FeedbackReadDTO
                           {
                               FeedbackId = f.FeedbackId,
                               UserId = f.Appointment.UserId,
                               Comment = f.Comment,
                               Rating = f.Rating,
                               ServiceId = f.Appointment.ServiceId,
                               AppointmentId = f.AppointmentId
                           });
        }

        public async Task<IEnumerable<FeedbackReadDTO>> GetByUserIdAsync(Guid userId)
        {
            var feedbacks = await _feedbackRepository.GetAllFeedbackAsync();
            return feedbacks.Where(f => f.Appointment.UserId == userId)
                           .Select(f => new FeedbackReadDTO
                           {
                               FeedbackId = f.FeedbackId,
                               UserId = f.Appointment.UserId,
                               Comment = f.Comment,
                               Rating = f.Rating,
                               ServiceId = f.Appointment.ServiceId,
                               AppointmentId = f.AppointmentId
                           });
        }
    }
} 