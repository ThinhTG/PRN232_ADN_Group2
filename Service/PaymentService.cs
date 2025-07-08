using Repository.Entity;
using Repository.Repository;
using Service.DTOs;
using Net.payOS;
using Net.payOS.Types;
using Service.Interface;
using Service.Auth;
using Core.enums;


namespace Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repo;
        private readonly PayOS _payOS;
        private readonly AuthService _authService;
        private readonly IAppointmentRepository _appointmentRepo;
        public PaymentService(IPaymentRepository repo, AuthService authService, PayOS payOS, IAppointmentRepository appointmentRepo)
        {
            _repo = repo;
            _authService = authService;
            _payOS = payOS;
            _appointmentRepo = appointmentRepo;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync() => await _repo.GetAllAsync();
        public async Task<Payment> GetByIdAsync(object id) => await _repo.GetByIdAsync(id);
        public async Task<Payment> AddAsync(Payment entity)
        {
            await _repo.AddAsync(entity);
            await _repo.SaveAsync();
            return entity;
        }
        public async Task<Payment> UpdateAsync(Payment entity)
        {
            _repo.Update(entity);
            await _repo.SaveAsync();
            return entity;
        }
        public async Task<bool> DeleteAsync(object id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null) return false;
            _repo.Delete(entity);
            await _repo.SaveAsync();
            return true;
        }

        public async Task<CreatePaymentResult> CreateLinkAsync(PaymentResponse request)
        {
            int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
            string accountId = _authService.GetUserId();
            var account =  await _repo.GetByIdAsync(Guid.Parse(accountId));
            Payment payment = new Payment
            {
                PaymentId = Guid.NewGuid(),
                OrderCode = orderCode,
                AppointmentId = Guid.Parse(request.AppointmentId),
                Amount = request.Price,
                PaidDate = DateTime.UtcNow,
                Status = PaymentStatus.Pending.ToString(),
            };
            await _repo.AddAsync(payment);
            await _repo.SaveAsync();
            ItemData item = new ItemData(accountId, 1, request.Price);
            var descriptions = request.Description = $"Booking {request.Price}";
            List<ItemData> items = new List<ItemData> { item };
            var expiredAt = DateTimeOffset.UtcNow.AddMinutes(15).ToUnixTimeSeconds();
            PaymentData paymentDataPayment = new PaymentData(orderCode, request.Price, descriptions, items, request.CancelUrl, request.ReturnUrl, null, null, null, null, null,expiredAt);
            try
            {
                var createdLink = await _payOS.createPaymentLink(paymentDataPayment);
                return createdLink;

            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        public async Task CheckStatusAsync(string orderCode)
        {
            var payment = await _repo.GetByOrderCodeAsync(int.Parse(orderCode));
            if (payment == null)
                throw new ArgumentException("Transaction not found");
            var checking = await _payOS.getPaymentLinkInformation(long.Parse(orderCode));

            if (checking.status == "PAID")
            {
                payment.Status = PaymentStatus.Completed.ToString();
                 _repo.Update(payment);
                await _repo.SaveAsync();
                var appointment = await _appointmentRepo.GetByIdAsync(payment.AppointmentId);
                appointment.Status = AppointmentStatus.Confirmed.ToString();
                _appointmentRepo.Update(appointment);
                await _appointmentRepo.SaveAsync();
            }
            else
            {
                throw new ArgumentException("Transaction is not paid yet");
            }

        }

    }
} 