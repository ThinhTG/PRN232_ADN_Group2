using System;

namespace Service.DTOs
{
    public class AppointmentCreateUpdateDTO
    {
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public int? Status { get; set; } // Allow nullable for updates
    }
} 