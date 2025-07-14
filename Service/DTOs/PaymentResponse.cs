namespace Service.DTOs
{
    public class PaymentResponse
    {
        public int Price { get; set; }
        public string AppointmentId { get; set; }
        public string? Description = "Appointment";
        public string ReturnUrl = "http://localhost:5173/payment-success";
        public string CancelUrl = "http://localhost:5173/payment-fail";
    }
}
