using Microsoft.EntityFrameworkCore;
using Repository.DBContext;
using Repository.Entity;

namespace Repository.Repository
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(ADNDbContext context) : base(context) { }

        public async Task<List<Address>> GetByUserIdAsync(Guid userId)
        {
            return await _dbSet.Where(a => a.UserId == userId).ToListAsync();
        }
    }
} 