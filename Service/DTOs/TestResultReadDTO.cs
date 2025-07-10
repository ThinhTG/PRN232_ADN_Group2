namespace Service.DTOs
{
    public class TestResultReadDTO
    {
        public int ResultId { get; set; }
        public Guid AppointmentId { get; set; }
        public DateTime ResultDate { get; set; }
        public string Description { get; set; }
    }
}
