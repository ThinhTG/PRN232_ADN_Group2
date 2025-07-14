using Core.enums;
using Repository.Entity;
using Repository.Repository;
using Service.DTOs;
using Service.Interface;

namespace Service
{
    public class TestResultService : ITestResultService
    {
        private readonly ITestResultRepository _repo;
        private readonly IAppointmentRepository _appointmentRepo;
        public TestResultService(ITestResultRepository repo, IAppointmentRepository appointmentRepo)
        {
            _repo = repo;
            _appointmentRepo = appointmentRepo;
        }

        public async Task<IEnumerable<TestResultReadDTO>> GetAllAsync()
        {
            var results = await _repo.GetAllAsync();
            return results.Select(r => MapToReadDTO(r));
        }

        public async Task<TestResultReadDTO> GetByIdAsync(object id)
        {
            var result = await _repo.GetByIdAsync(id);
            return result == null ? null : MapToReadDTO(result);
        }

        public async Task<IEnumerable<TestResultReadDTO>> GetByAppointmentIdAsync(Guid appointmentId)
        {
            var results = await _repo.GetByAppointmentIdAsync(appointmentId);
            return results.Select(r => MapToReadDTO(r));
        }

        public async Task<TestResultReadDTO> AddAsync(TestResultCreateUpdateDTO dto)
        {
            var entity = new TestResult
            {
                AppointmentId = dto.AppointmentId,
                ResultDate = dto.ResultDate,
                Description = dto.Description
            };

            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            Appointment appointment = await _appointmentRepo.GetByIdAsync(dto.AppointmentId);
            appointment.Status = AppointmentStatus.Completed.ToString();
            await _appointmentRepo.SaveAsync();
            return MapToReadDTO(entity);
        }

        public async Task<TestResultReadDTO> UpdateAsync(int id,TestResultCreateUpdateDTO dto)
        {
            // Giả định rằng DTO có chứa `ResultId` (bạn nên thêm vào class)
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return null;
            existing.AppointmentId = dto.AppointmentId;
            existing.ResultDate = dto.ResultDate;
            existing.Description = dto.Description;
            
            _repo.Update(existing);
            await _repo.SaveAsync();

            return MapToReadDTO(existing);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            _repo.Delete(entity);
            await _repo.SaveAsync();
            return true;
        }

        private TestResultReadDTO MapToReadDTO(TestResult entity) => new()
        {
            ResultId = entity.ResultId,
            AppointmentId = entity.AppointmentId,
            ResultDate = entity.ResultDate,
            Description = entity.Description
        };
    }
}
