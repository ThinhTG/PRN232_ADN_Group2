using System;

namespace Repository.Entity
{
    public class TestResult
    {
        public int ResultId { get; set; }
        public Guid AppointmentId { get; set; }
        public DateTime ResultDate { get; set; }
        public string Description { get; set; }

        public Appointment Appointment { get; set; }
    }
} 