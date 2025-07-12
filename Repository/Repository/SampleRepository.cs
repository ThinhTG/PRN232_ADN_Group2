using Microsoft.EntityFrameworkCore;
using Repository.DBContext;
using Repository.Entity;

namespace Repository.Repository
{
    public class SampleRepository : GenericRepository<Sample>, ISampleRepository
    {
        public SampleRepository(ADNDbContext context) : base(context) { }

        public async Task<List<Sample>> GetByIdsAsync(List<Guid> ids)
        {
            return await _dbSet.Where(s => ids.Contains(s.SampleId)).ToListAsync();
        }
    }
} 