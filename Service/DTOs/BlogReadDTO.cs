using System;

namespace Service.DTOs
{
    public class BlogReadDTO
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public DateTime PublishedDate { get; set; }
    }
} 