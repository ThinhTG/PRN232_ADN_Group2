using System;

namespace Service.DTOs
{
    public class FeedbackCreateUpdateDTO
    {
        public Guid UserId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public Guid ServiceId { get; set; }
    }
} 