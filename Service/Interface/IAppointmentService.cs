using Service.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentReadDTO>> GetAllAsync();
        Task<AppointmentReadDTO> GetByIdAsync(Guid id);
        Task<AppointmentReadDTO> AddAsync(AppointmentCreateUpdateDTO entity);
        Task<bool> UpdateAsync(Guid id, AppointmentCreateUpdateDTO entity);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<AppointmentReadDTO>> GetAppointmentByUserIdAsync(Guid userId);
    }
} 