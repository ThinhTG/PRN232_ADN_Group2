using System;

namespace Repository.Entity
{
    public class TestPerson
    {
        public Guid PersonId { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public string Relationship { get; set; }
        public Guid AppointmentId { get; set; }

        public Appointment Appointment { get; set; }
    }
} 