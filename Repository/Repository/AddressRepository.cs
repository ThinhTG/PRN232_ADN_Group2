using Repository.DBContext;
using Repository.Entity;

namespace Repository.Repository
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(ADNDbContext context) : base(context) { }
    }
} 