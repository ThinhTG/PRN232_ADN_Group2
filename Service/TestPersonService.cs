using Repository.Entity;
using Repository.Repository;
using Service.DTOs;
using Service.Interface;

namespace Service
{
    public class TestPersonService : ITestPersonService
    {
        private readonly ITestPersonRepository _repo;

        public TestPersonService(ITestPersonRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TestPersonReadDTO>> GetAllAsync()
        {
            var entities = await _repo.GetAllAsync();
            return entities.Select(MapToReadDTO);
        }

        public async Task<TestPersonReadDTO> GetByIdAsync(object id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : MapToReadDTO(entity);
        }

        public async Task<List<TestPersonReadDTO>> AddAsync(List<TestPersonCreateUpdateDTO> dto)
        {
            List<TestPersonReadDTO>  listResponse= new();
            foreach(var item in dto)
            {
                var entity = MapToEntity(item);
                entity.PersonId = Guid.NewGuid(); // tự tạo ID
                listResponse.Add(MapToReadDTO(entity));
                await _repo.AddAsync(entity);
            } 
            await _repo.SaveAsync();
            return listResponse;
        }

        public async Task<TestPersonReadDTO> UpdateAsync(Guid id,TestPersonCreateUpdateDTO dto)
        {
    
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return null;

            entity.FullName = dto.FullName;
            entity.Gender = dto.Gender;
            entity.Relationship = dto.Relationship;
            entity.AppointmentId = dto.AppointmentId;

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

        // Mapping methods
        private TestPersonReadDTO MapToReadDTO(TestPerson entity)
        {
            return new TestPersonReadDTO
            {
                PersonId = entity.PersonId,
                FullName = entity.FullName,
                Gender = entity.Gender,
                Relationship = entity.Relationship,
                AppointmentId = entity.AppointmentId
            };
        }

        private TestPerson MapToEntity(TestPersonCreateUpdateDTO dto)
        {
            return new TestPerson
            {
                FullName = dto.FullName,
                Gender = dto.Gender,
                Relationship = dto.Relationship,
                AppointmentId = dto.AppointmentId
            };
        }
    }
}
