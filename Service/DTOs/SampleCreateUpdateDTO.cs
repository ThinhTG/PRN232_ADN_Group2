namespace Service.DTOs
{
    public class SampleCreateUpdateDTO
    {
        public Guid KitId { get; set; }
        public DateTime CollectedDate { get; set; }
        public DateTime ReceivedDate { get; set; }

        public Guid PersonId { get; set; }
    }
}
