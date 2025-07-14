using System;

namespace Service.DTOs
{
    public class BlogCreateUpdateDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public Guid UserId { get; set; }
    }
} 