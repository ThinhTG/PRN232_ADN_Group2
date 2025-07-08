using System;

namespace Service.DTOs
{
    public class AppointmentCreateUpdateDTO
    {
        public Guid ServiceId { get; set; }
        public DateTime ScheduleDate { get; set; }
    }
} 