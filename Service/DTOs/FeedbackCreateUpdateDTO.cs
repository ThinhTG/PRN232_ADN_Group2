using System;

namespace Service.DTOs
{
    /// DTO dùng để tạo/cập nhật feedback cho một appointment cụ thể
    public class FeedbackCreateUpdateDTO
    {
        public Guid UserId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public Guid AppointmentId { get; set; }
    }
} 