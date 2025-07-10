using Repository.Entity;

namespace Service.DTOs
{
    public class SampleReadDTO
    {
        public Guid SampleId { get; set; }
        public Guid KitId { get; set; }
        public DateTime CollectedDate { get; set; }
        public DateTime ReceivedDate { get; set; }

        public Guid PersonId { get; set; }
    }
}
