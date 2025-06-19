using System;

namespace Repository.Entity
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public Guid AppointmentId { get; set; }
        public decimal Amount { get; set; }
        public bool Status { get; set; }
        public DateTime PaidDate { get; set; }

        public Appointment Appointment { get; set; }
    }
} 