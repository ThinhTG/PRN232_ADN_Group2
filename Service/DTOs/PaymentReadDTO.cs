using System;

namespace Service.DTOs
{
    public class PaymentReadDTO
    {
        public Guid PaymentId { get; set; }
        public Guid AppointmentId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime? PaidDate { get; set; }
    }
} 