using System;

namespace Repository.Entity
{
    public class SampleKit
    {
        public int Id { get; set; }
        public Guid? FeedbackId { get; set; }
        public Feedback Feedback { get; set; }
    }
} 