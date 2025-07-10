namespace Service.DTOs
{
    public class TestPersonCreateUpdateDTO
    {
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public string Relationship { get; set; }
        public Guid AppointmentId { get; set; }
    }
}
