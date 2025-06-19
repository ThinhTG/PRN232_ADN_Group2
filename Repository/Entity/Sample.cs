using System;

namespace Repository.Entity
{
    public class Sample
    {
        public Guid SampleId { get; set; }
        public Guid KitId { get; set; }
        public DateTime CollectedDate { get; set; }
        public DateTime ReceivedDate { get; set; }

        public Kit Kit { get; set; }
    }
} 