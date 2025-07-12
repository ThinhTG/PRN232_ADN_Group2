using Repository.Repository;
using Service.DTOs;
using Service.Interface;

namespace Service
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<ServiceReadDTO> AddAsync(ServiceCreateUpdateDTO dto)
        {
            var entity = new Repository.Entity.Service
            {
                ServiceId = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                AllowHomeKit = dto.AllowHomeKit,
                Price = dto.Price,
                Type = dto.Type,
                Url = dto.Url,
                CreatedAt = DateTime.UtcNow
            };

            await _serviceRepository.AddAsync(entity);
            await _serviceRepository.SaveAsync();

            return new ServiceReadDTO
            {
                Url = entity.Url,
                ServiceId = entity.ServiceId,
                Name = entity.Name,
                Description = entity.Description,
                AllowHomeKit = entity.AllowHomeKit,
                Price = entity.Price,
                Type = entity.Type,
                CreatedAt = entity.CreatedAt
            };
        }

        public async Task<IEnumerable<ServiceReadDTO>> GetAllAsync()
        {
            var services = await _serviceRepository.GetAllAsync();
            return services.Select(s => new ServiceReadDTO
            {
                ServiceId = s.ServiceId,
                Name = s.Name,
                Description = s.Description,
                AllowHomeKit = s.AllowHomeKit,
                Price = s.Price,
                Url = s.Url,
                Type = s.Type,
                CreatedAt = s.CreatedAt
            });
        }

        public  async Task<ServiceReadDTO> GetByIdAsync(Guid serviceId)
        {
            var s = await _serviceRepository.GetByIdAsync(serviceId);
            return  new ServiceReadDTO
            {
                ServiceId = s.ServiceId,
                Name = s.Name,
                Description = s.Description,
                AllowHomeKit = s.AllowHomeKit,
                Price = s.Price,
                Url = s.Url,
                Type = s.Type,
                CreatedAt = s.CreatedAt
            };
        }
    }
}
