using Core.enums;
using Repository.Entity;
using Repository.Repository;
using Service.Auth;
using Service.DTOs;
using Service.Interface;

namespace Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _repo;
        private readonly IServiceRepository _serviceRepo;
        private readonly UserRepository _userRepository;
        private readonly AuthService _authService;
        private readonly IAddressRepository _addressRepository;
        public AppointmentService(IAppointmentRepository repo, IServiceRepository serviceRepo, UserRepository userRepository, AuthService authService, IAddressRepository addressRepository)
        {
            _repo = repo;
            _serviceRepo = serviceRepo;
            _userRepository = userRepository;
            _authService = authService;
            _addressRepository = addressRepository;
        }

        public async Task<IEnumerable<AppointmentReadDTO>> GetAllAsync(bool? isHomeKit, AppointmentStatus? status)
        {
            var appointments = await _repo.GetAllAppointment(isHomeKit, status);
            return appointments.Select(a => new AppointmentReadDTO
            {
                AppointmentId = a.AppointmentId,
                UserId = a.UserId,
                ServiceId = a.ServiceId,
                ScheduleDate = a.ScheduleDate,
                IsHomeKit = a.IsHomeKit,
                Status = a.Status,
                TotalPrice = a.Service != null ? a.Service.Price : 0,
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
                IsHomeKit = a.IsHomeKit,
                ScheduleDate = a.ScheduleDate,
                Status = a.Status,
                TotalPrice = a.Service != null ? a.Service.Price : 0,
                BookingDate = a.BookingDate
            };
        }

        public async Task<AppointmentReadDTO> AddAsync(AppointmentCreateUpdateDTO dto)
        {
            // Nếu là HomeKit, kiểm tra user đã có địa chỉ chưa
            if (dto.IsHomeKit)
            {
                var userId = Guid.Parse(_authService.GetUserId());
                var addresses = await _addressRepository.GetByUserIdAsync(userId);
                if (addresses == null || addresses.Count == 0)
                {
                    throw new InvalidOperationException("You must add your address before booking a home kit appointment.");
                }
            }

            var appointment = new Appointment
            {
                AppointmentId = Guid.NewGuid(),
                UserId = Guid.Parse(_authService.GetUserId()),
                ServiceId = dto.ServiceId,
                IsHomeKit = dto.IsHomeKit,
                ScheduleDate = dto.ScheduleDate,
                Status = AppointmentStatus.Pending.ToString(), // Default status to 0 (e.g., Pending)
                BookingDate = DateTime.UtcNow
            };

            await _repo.AddAsync(appointment);
            await _repo.SaveAsync();
            var sv = await _serviceRepo.GetByIdAsync(dto.ServiceId);
            return new AppointmentReadDTO
            {
                AppointmentId = appointment.AppointmentId,
                UserId = appointment.UserId,
                ServiceId = appointment.ServiceId,
                ScheduleDate = appointment.ScheduleDate,
                IsHomeKit = appointment.IsHomeKit,
                Status = appointment.Status,
                BookingDate = appointment.BookingDate,
                TotalPrice = sv.Price
            };
        }

        public async Task<bool> UpdateAsync(Guid id, AppointmentCreateUpdateDTO dto)
        {
            var appointment = await _repo.GetByIdAsync(id);
            if (appointment == null) return false;

            appointment.UserId = Guid.Parse(_authService.GetUserId());
            appointment.ServiceId = dto.ServiceId;
            appointment.ScheduleDate = dto.ScheduleDate;
            
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

        public async Task<IEnumerable<AppointmentReadDTO>> GetAppointmentByUserIdAsync(Guid userId)
        {
            var appointments = await _repo.GetByUserIdAsync(userId);
            return appointments.Select(a => new AppointmentReadDTO
            {
                AppointmentId = a.AppointmentId,
                UserId = a.UserId,
                ServiceId = a.ServiceId,
                ScheduleDate = a.ScheduleDate,
                Status = a.Status,
                IsHomeKit = a.IsHomeKit,
                TotalPrice = a.Service != null ? a.Service.Price : 0,
                BookingDate = a.BookingDate
            });
        }
    }
} 