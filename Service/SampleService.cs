using Core.enums;
using Repository.Entity;
using Repository.Repository;
using Service.DTOs;
using Service.Interface;
using System.Linq;

namespace Service
{
    public class SampleService : ISampleService
    {
        private readonly ISampleRepository _repo;
        private readonly IAppointmentRepository _appointmentRepo;

        public SampleService(ISampleRepository repo, IAppointmentRepository appointmentRepo)
        {
            _repo = repo;
            _appointmentRepo = appointmentRepo;
        }

        public async Task<IEnumerable<SampleReadDTO>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return entities.Select(MapToReadDTO);
        }
        public async Task<IEnumerable<SampleReadDTO>> GetSampleByAppointmentIdAsync(Guid appointmentId)
        {
            var entities = await _repo.GetByAppointmentIdAsync(appointmentId);
            return entities.Select(MapToReadDTO);
        }
        public async Task<SampleReadDTO> GetByIdAsync(object id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : MapToReadDTO(entity);
        }

        public async Task<SampleReadDTO> AddAsync(SampleCreateUpdateDTO dto)
        {
            var entity = MapToEntity(dto);
            entity.SampleId = Guid.NewGuid(); // tạo ID mới

            await _repo.AddAsync(entity);
            await _repo.SaveAsync();

            return MapToReadDTO(entity);
        }

        public async Task<SampleReadDTO> UpdateAsync(Guid id, SampleCreateUpdateDTO dto)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;

            // cập nhật giá trị
            entity.KitId = dto.KitId;
            entity.CollectedDate = dto.CollectedDate;
            entity.ReceivedDate = dto.ReceivedDate;
            entity.PersonId = dto.PersonId;

            _repo.Update(entity);
            await _repo.SaveAsync();

            return MapToReadDTO(entity);
        }

        public async Task<bool> DeleteAsync(object id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;

            _repo.Delete(entity);
            await _repo.SaveAsync();
            return true;
        }

        public async Task CollectSamplesAsync(SampleCollectDTO dto)
        {
            // Lấy danh sách sample
            var samples = await _repo.GetByIdsAsync(dto.SampleIds);
            foreach (var sample in samples)
            {
                sample.CollectedDate = DateTime.UtcNow;
                _repo.Update(sample);
            }
            await _repo.SaveAsync();

            // Cập nhật trạng thái Appointment sang Collected
            var appointment = await _appointmentRepo.GetByIdAsync(dto.AppointmentId);
            if (appointment != null)
            {
                appointment.Status = "Collected";
                _appointmentRepo.Update(appointment);
                await _appointmentRepo.SaveAsync();
            }
        }

        public async Task ReceiveSamplesAsync(SampleReceiveDTO dto)
        {
            var samples = await _repo.GetByIdsAsync(dto.SampleIds);
            foreach (var sample in samples)
            {
                sample.ReceivedDate = DateTime.UtcNow;
                _repo.Update(sample);
            }
            await _repo.SaveAsync();

            // Cập nhật trạng thái Appointment sang InProgress
            if (samples.Count > 0)
            {
                var kit = samples[0].Kit;
                if (kit != null)
                {
                    var appointment = await _appointmentRepo.GetByIdAsync(kit.AppointmentId);
                    if (appointment != null)
                    {
                        appointment.Status = "InProgress";
                        _appointmentRepo.Update(appointment);
                        await _appointmentRepo.SaveAsync();
                    }
                }
            }
        }


        private SampleReadDTO MapToReadDTO(Sample entity)
        {
            return new SampleReadDTO
            {
                SampleId = entity.SampleId,
                KitId = entity.KitId,
                CollectedDate = entity.CollectedDate,
                ReceivedDate = entity.ReceivedDate,
                PersonId = entity.PersonId
            };
        }

        private Sample MapToEntity(SampleCreateUpdateDTO dto)
        {
            return new Sample
            {
                KitId = dto.KitId,
                CollectedDate = dto.CollectedDate,
                ReceivedDate = dto.ReceivedDate,
                PersonId = dto.PersonId
            };
        }

        public async Task CollectOnsiteSamplesAsync(SampleCollectDTO dto)
        {

            var samples = await _repo.GetByIdsAsync(dto.SampleIds);
            foreach (var sample in samples)
            {
                sample.CollectedDate = DateTime.UtcNow;
                sample.ReceivedDate = DateTime.UtcNow;
                _repo.Update(sample);
            }
            await _repo.SaveAsync();

            // Cập nhật trạng thái Appointment sang InProgress
            var appointment = await _appointmentRepo.GetByIdAsync(dto.AppointmentId);
            if (appointment != null)
            {
                appointment.Status = AppointmentStatus.InProgress.ToString();
                _appointmentRepo.Update(appointment);
                await _appointmentRepo.SaveAsync();
            }
        }
    }
}
