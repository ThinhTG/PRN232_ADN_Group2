using System;
using System.ComponentModel.DataAnnotations;

namespace Service.DTOs
{
    /// DTO dùng để tạo/cập nhật feedback cho một appointment cụ thể
    public class FeedbackCreateUpdateDTO
    {
        [Required(ErrorMessage = "UserId is required")]
        public Guid UserId { get; set; }
        
        [Required(ErrorMessage = "Comment is required")]
        [StringLength(500, ErrorMessage = "Comment cannot exceed 500 characters")]
        public string Comment { get; set; }
        
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; }
        
        [Required(ErrorMessage = "AppointmentId is required")]
        public Guid AppointmentId { get; set; }
    }
} 