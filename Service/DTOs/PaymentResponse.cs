namespace Service.DTOs
{
    public class PaymentResponse
    {
        public int Price { get; set; }
        public string AppointmentId { get; set; }
        public string? Description = "Appointment";
        public string ReturnUrl = "https://mystic-blind-box.web.app/wallet-success";
        public string CancelUrl = "https://mystic-blind-box.web.app/wallet-fail";
    }
}
