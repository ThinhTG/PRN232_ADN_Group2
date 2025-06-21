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
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;
        public AppointmentService(IAppointmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<AppointmentReadDTO>> GetAllAsync()
        {
            var appointments = await _repo.GetAllAsync();
            return appointments.Select(a => new AppointmentReadDTO
            {
                AppointmentId = a.AppointmentId,
                UserId = a.UserId,
                ServiceId = a.ServiceId,
                ScheduleDate = a.ScheduleDate,
                Status = a.Status,
                BookingDate = a.BookingDate
            });
        }

        public async Task<AppointmentReadDTO> GetByIdAsync(Guid id)
        {
            var a = await _repo.GetByIdAsync(id);
            if (a == null) return null;
            return new AppointmentReadDTO
            {
                AppointmentId = a.AppointmentId,
                UserId = a.UserId,
                ServiceId = a.ServiceId,
                ScheduleDate = a.ScheduleDate,
                Status = a.Status,
                BookingDate = a.BookingDate
            };
        }

        public async Task<AppointmentReadDTO> AddAsync(AppointmentCreateUpdateDTO dto)
        {
            var appointment = new Appointment
            {
                AppointmentId = Guid.NewGuid(),
                UserId = dto.UserId,
                ServiceId = dto.ServiceId,
                ScheduleDate = dto.ScheduleDate,
                Status = dto.Status ?? 0, // Default status to 0 (e.g., Pending)
                BookingDate = DateTime.UtcNow
            };

            await _repo.AddAsync(appointment);
            await _repo.SaveAsync();

            return new AppointmentReadDTO
            {
                AppointmentId = appointment.AppointmentId,
                UserId = appointment.UserId,
                ServiceId = appointment.ServiceId,
                ScheduleDate = appointment.ScheduleDate,
                Status = appointment.Status,
                BookingDate = appointment.BookingDate
            };
        }

        public async Task<bool> UpdateAsync(Guid id, AppointmentCreateUpdateDTO dto)
        {
            var appointment = await _repo.GetByIdAsync(id);
            if (appointment == null) return false;

            appointment.UserId = dto.UserId;
            appointment.ServiceId = dto.ServiceId;
            appointment.ScheduleDate = dto.ScheduleDate;
            if(dto.Status.HasValue)
            {
                appointment.Status = dto.Status.Value;
            }
            
            _repo.Update(appointment);
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