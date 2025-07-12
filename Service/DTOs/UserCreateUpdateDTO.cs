namespace Service.DTOs
{
    public class UserCreateUpdateDTO
    {
        public string? FullName { get; set; }
        public string Email { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Gender { get; set; }
    }
}
