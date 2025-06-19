using ADN_Group2.BusinessObject.Identity;
using System;

namespace Repository.Entity
{
    public class Feedback
    {
        public Guid FeedbackId { get; set; }
        public Guid UserId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public Guid ServiceId { get; set; }

        public ApplicationUser User { get; set; }
        public Service Service { get; set; }
    }
} 