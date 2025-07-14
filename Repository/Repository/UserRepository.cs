using ADN_Group2.BusinessObject.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.DBContext;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class UserRepository 
    {
        private readonly ADNDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(ADNDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            return await _userManager.UpdateAsync(user);
        }
        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await _userManager.Users
                .Include(u => u.Addresses)
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .ToListAsync();
        }
        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<ApplicationUser> FindByIdAsync(Guid? id)
        {
            return await _userManager.Users.Include(x => x.Addresses).FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task AddToRoleAsync(ApplicationUser user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
    }
} 