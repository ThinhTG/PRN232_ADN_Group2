using System;
using System.Collections.Generic;

namespace Service.DTOs
{
    public class SampleCollectDTO
    {
        public Guid AppointmentId { get; set; }
        public List<Guid> SampleIds { get; set; }
    }
} 