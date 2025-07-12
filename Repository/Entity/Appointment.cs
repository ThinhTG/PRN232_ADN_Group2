using ADN_Group2.BusinessObject.Identity;

namespace Repository.Entity
{
    public class Appointment
    {
        public Guid AppointmentId { get; set; }
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public string Status { get; set; }
        public DateTime BookingDate { get; set; }
        public bool IsHomeKit { get; set; }
        public ApplicationUser User { get; set; }
        public Service Service { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<TestResult> TestResults { get; set; }
        public ICollection<Kit> Kits { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
    }
} 