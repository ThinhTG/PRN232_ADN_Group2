namespace Service.DTOs
{
    public class TestResultCreateUpdateDTO
    {
        public Guid AppointmentId { get; set; }
        public DateTime ResultDate { get; set; }
        public string Description { get; set; }
    }
}
