using ADN_Group2.BusinessObject.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Entity;
using Repository.Repository;
using Service.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Auth
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(UserRepository userRepository, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            
            if (user == null)
                throw new Exception("HttpContext.User is null");
            if (!user.Identity?.IsAuthenticated == true)
                throw new Exception("User is not authenticated. Claims: " + string.Join(", ", user.Claims.Select(c => $"{c.Type}:{c.Value}")));
            var id = user.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value
      ?? user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return id ?? throw new Exception("User ID not found in token.");

        }
        public async Task<(bool Success, string Token, string Error)> LoginAsync(string email, string password)
        {
            var user = await _userRepository.FindByEmailAsync(email);
            if (user == null)
                return (false, null, "User not found");

            if (!await _userRepository.CheckPasswordAsync(user, password))
                return (false, null, "Invalid password");

            var token = await GenerateJwtToken(user);
            return (true, token, null);
        }

        public async Task<(bool Success, string Error)> RegisterAsync(string email, string password, string fullName, string role)
        {
            var user = new ApplicationUser
            {
                Email = email,
                UserName = email,
                FullName = fullName
            };
            var result = await _userRepository.CreateUserAsync(user, password);
            if (!result.Succeeded)
                return (false, string.Join(", ", result.Errors.Select(e => e.Description)));

            await _userRepository.AddToRoleAsync(user, role);

            return (true, null);
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var claims = new List<Claim>
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                            new Claim(JwtRegisteredClaimNames.Email, user.Email),
                            new Claim("FullName", user.FullName ?? ""),
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) // Thêm dòng này
                        };

            var roles = await _userRepository.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<UserReadDTO> GetUserById(Guid? id)
        {
            if(id == null)
            {
                id = Guid.Parse(GetUserId());
            }
            var user = _userRepository.FindByIdAsync(id).Result;
            if (user == null) return null;
            return new UserReadDTO
            {
                 Id = user.Id,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl,
                FullName = user.FullName,
                Gender = user.Gender,
                Role = _userRepository.GetRolesAsync(user).Result.FirstOrDefault(),
                Addresses = GetUserAddressAsync(user.Addresses).ToList(),
            };
        }
        public  IEnumerable<AddressReadDTO> GetUserAddressAsync(ICollection<Address> addresses)
        {
            return addresses.Select(a => new AddressReadDTO
            {
                AddressId = a.AddressId,
                Number = a.Number,
                District = a.District,
                Province = a.Province,
                UserId = a.UserId
            });
        }
        public async Task<IdentityResult> UpdateUserAsync(Guid? userId, UserCreateUpdateDTO dto)
        {
            if (userId == null)
            {
                userId = Guid.Parse(GetUserId());
            }
            var user = await _userRepository.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "User not found"
                });
            }

            if (!string.IsNullOrWhiteSpace(dto.FullName))
                user.FullName = dto.FullName;

            if (!string.IsNullOrWhiteSpace(dto.AvatarUrl))
                user.AvatarUrl = dto.AvatarUrl;

            if (!string.IsNullOrWhiteSpace(dto.Gender))
                user.Gender = dto.Gender;

            user.LastUpdatedTime = DateTimeOffset.UtcNow;

            return await _userRepository.UpdateUserAsync(user);
        }

    }
} 