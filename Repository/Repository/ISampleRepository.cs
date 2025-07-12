using Repository.Entity;

namespace Repository.Repository
{
    public interface ISampleRepository : IGenericRepository<Sample>
    {
        Task<List<Sample>> GetByIdsAsync(List<Guid> ids);
    }
} 