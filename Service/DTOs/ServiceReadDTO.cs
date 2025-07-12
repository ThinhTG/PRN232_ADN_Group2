namespace Service.DTOs
{
    public class ServiceReadDTO
    {
        public Guid ServiceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool AllowHomeKit { get; set; }
        public decimal Price { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Type { get; set; }
    }
}
