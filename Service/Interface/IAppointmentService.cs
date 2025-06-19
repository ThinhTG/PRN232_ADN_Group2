using Repository.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAppointmentService
    {
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<Appointment> GetByIdAsync(object id);
        Task<Appointment> AddAsync(Appointment entity);
        Task<Appointment> UpdateAsync(Appointment entity);
        Task<bool> DeleteAsync(object id);
    }
} 