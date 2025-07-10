using Repository.Entity;
using Repository.Repository;
using Service.DTOs;
using Service.Interface;

namespace Service
{
    public class SampleService : ISampleService
    {
        private readonly ISampleRepository _repo;

        public SampleService(ISampleRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<SampleReadDTO>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
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
    }
}
