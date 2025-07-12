using System;

namespace Service.DTOs
{
    /// DTO dùng để đọc thông tin feedback cho một appointment cụ thể
    public class FeedbackReadDTO
    {
        public Guid FeedbackId { get; set; }
        public Guid UserId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public Guid ServiceId { get; set; }
        public Guid AppointmentId { get; set; }
    }
} 