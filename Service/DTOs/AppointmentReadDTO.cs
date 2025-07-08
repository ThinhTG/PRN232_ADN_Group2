using System;

namespace Service.DTOs
{
    public class AppointmentReadDTO
    {
        public Guid AppointmentId { get; set; }
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string Status { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalPrice { get; set; } // Assuming TotalPrice is part of the appointment details
    }
} 