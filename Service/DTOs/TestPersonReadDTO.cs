namespace Service.DTOs
{
    public class TestPersonReadDTO
    {
        public Guid PersonId { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public string Relationship { get; set; }
        public Guid AppointmentId { get; set; }
    }
}
