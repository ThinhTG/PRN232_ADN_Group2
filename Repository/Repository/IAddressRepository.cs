using Repository.Entity;

namespace Repository.Repository
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        Task<List<Address>> GetByUserIdAsync(Guid userId);
    }
} 