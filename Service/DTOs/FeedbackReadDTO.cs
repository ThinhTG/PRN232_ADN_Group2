using System;

namespace Service.DTOs
{
    public class FeedbackReadDTO
    {
        public Guid FeedbackId { get; set; }
        public Guid UserId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public Guid ServiceId { get; set; }
    }
} 