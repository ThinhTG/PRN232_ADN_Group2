using System;
using System.Collections.Generic;

namespace Repository.Entity
{
    public class Kit
    {
        public Guid KitId { get; set; }
        public Guid AppointmentId { get; set; }
        public long TrackingNumber { get; set; }

        public Appointment Appointment { get; set; }
        public ICollection<Sample> Samples { get; set; }
    }
} 