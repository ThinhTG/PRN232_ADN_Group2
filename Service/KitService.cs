using Repository.Entity;
using Repository.Repository;
using Service.DTOs;
using Service.Interface;

namespace Service
{
    public class KitService : IKitService
    {
        private readonly IKitRepository _repo;
        public KitService(IKitRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<KitReadDTO>> GetAllAsync()
        {
            var kits = await _repo.GetAllAsync();
            return kits.Select(k => new KitReadDTO
            {
                KitId = k.KitId,
                AppointmentId = k.AppointmentId,
                TrackingNumber = k.TrackingNumber
            });
        }

        public async Task<KitReadDTO> GetByIdAsync(Guid id)
        {
            var k = await _repo.GetByIdAsync(id);
            if (k == null) return null;
            return new KitReadDTO
            {
                KitId = k.KitId,
                AppointmentId = k.AppointmentId,
                TrackingNumber = k.TrackingNumber
            };
        }

        public async Task<KitReadDTO> AddAsync(KitCreateUpdateDTO dto)
        {
            var kit = new Kit
            {
                KitId = Guid.NewGuid(),
                AppointmentId = dto.AppointmentId,
                TrackingNumber = dto.TrackingNumber,
            };

            await _repo.AddAsync(kit);
            await _repo.SaveAsync();

            return new KitReadDTO
            {
                KitId = kit.KitId,
                AppointmentId = kit.AppointmentId,
                TrackingNumber = kit.TrackingNumber
            };
        }

        public async Task<bool> UpdateAsync(Guid id, KitCreateUpdateDTO dto)
        {
            var kit = await _repo.GetByIdAsync(id);
            if (kit == null) return false;

            kit.AppointmentId = dto.AppointmentId;
            kit.TrackingNumber = dto.TrackingNumber;

            _repo.Update(kit);
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