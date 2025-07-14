using System;

namespace Service.DTOs
{
    public class KitReadDTO
    {
        public Guid KitId { get; set; }
        public Guid AppointmentId { get; set; }
        public long TrackingNumber { get; set; }
    }
} 