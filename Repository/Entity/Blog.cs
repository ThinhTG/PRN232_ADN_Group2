using ADN_Group2.BusinessObject.Identity;
using System;

namespace Repository.Entity
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public string? Url { get; set; }
        public DateTime PublishedDate { get; set; }

        public ApplicationUser User { get; set; }
    }
} 