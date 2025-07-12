namespace Service.DTOs
{
    public class UserReadDTO
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }

        public string? AvatarUrl { get; set; }
        public string? Gender { get; set; }
        public string Email { get; set; }
        public string? Role { get; set; }
        public List<AddressReadDTO> Addresses { get; set; }
    }
}
