namespace Repository.Entity
{
    public class Service
    {
        public Guid ServiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AllowHomeKit { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
} 