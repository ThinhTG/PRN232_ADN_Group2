using ADN_Group2.BusinessObject.Identity;
using System;

namespace Repository.Entity
{
    public class Feedback
    {
        public Guid FeedbackId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public Guid AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
} 