using Repository.Entity;
using Repository.Repository;
using Service.Interface;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Appointment>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Appointment> GetByIdAsync(object id) => await _repo.GetByIdAsync(id);
        public async Task<Appointment> AddAsync(Appointment entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            return entity;
        }
        public async Task<Appointment> UpdateAsync(Appointment entity)
        {
            _repo.Update(entity);
            await _repo.SaveAsync();
            return entity;
        }
        public async Task<bool> DeleteAsync(object id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;
            _repo.Delete(entity);
            await _repo.SaveAsync();
            return true;
        }
    }
} 