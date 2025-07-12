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
        private readonly IKitRepository _kitRepository;
        private readonly ISampleRepository _sampleRepository;
        private readonly AuthService _authService;
        private readonly ITestPersonRepository _testPersonRepository;
        private readonly IAppointmentRepository _appointmentRepo;
        public PaymentService(IPaymentRepository repo, AuthService authService, PayOS payOS, IAppointmentRepository appointmentRepo, ISampleRepository sampleRepository, IKitRepository kitRepository, ITestPersonRepository testPersonRepository)
        {
            _repo = repo;
            _authService = authService;
            _payOS = payOS;
            _appointmentRepo = appointmentRepo;
            _sampleRepository = sampleRepository;
            _kitRepository = kitRepository;
            _testPersonRepository = testPersonRepository;
        }

        public async Task<IEnumerable<PaymentReadDTO>> GetAllAsync()
        {
            var payments = await _repo.GetAllAsync();
            return payments.Select(MapToReadDTO);
        }

        public async Task<PaymentReadDTO> GetByIdAsync(object id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity == null ? null : MapToReadDTO(entity);
        }

        // Mapping
        private PaymentReadDTO MapToReadDTO(Payment entity)
        {
            return new PaymentReadDTO
            {
                PaymentId = entity.PaymentId,
                AppointmentId = entity.AppointmentId,
                Amount = entity.Amount,
                Status = entity.Status,
                PaidDate = entity.PaidDate
            };
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
            var account = await _repo.GetByIdAsync(Guid.Parse(accountId));
            Payment payment = new Payment
            {
                PaymentId = Guid.NewGuid(),
                OrderCode = orderCode,
                AppointmentId = Guid.Parse(request.AppointmentId),
                Amount = request.Price,
                Status = PaymentStatus.Pending.ToString(),
            };
            await _repo.AddAsync(payment);
            await _repo.SaveAsync();
            ItemData item = new ItemData(accountId, 1, request.Price);
            var descriptions = request.Description = $"Booking {request.Price}";
            List<ItemData> items = new List<ItemData> { item };
            var expiredAt = DateTimeOffset.UtcNow.AddMinutes(15).ToUnixTimeSeconds();
            PaymentData paymentDataPayment = new PaymentData(orderCode, request.Price, descriptions, items, request.CancelUrl, request.ReturnUrl, null, null, null, null, null, expiredAt);
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
                payment.PaidDate = DateTime.UtcNow;
                _repo.Update(payment);
                await _repo.SaveAsync();
                var appointment = await _appointmentRepo.GetByIdAsync(payment.AppointmentId);
                appointment.Status = AppointmentStatus.WaitingToCollect.ToString();
                _appointmentRepo.Update(appointment);
                await _appointmentRepo.SaveAsync();
                Kit kit = await AddKitAsync(appointment.AppointmentId, long.Parse(orderCode));
                List<Guid> list = await _testPersonRepository.GetTestPersonIdByAppointment(appointment.AppointmentId);
                foreach (var item in list)
                {
                  await  AddSampleAsync(kit.KitId, item, appointment.BookingDate);
                }
            }
            else
            {
                throw new ArgumentException("Transaction is not paid yet");
            }

        }
        public async Task<Kit> AddKitAsync(Guid appointmentId, long trackingNumber)
        {
            var kit = new Kit
            {
                KitId = Guid.NewGuid(),
                AppointmentId = appointmentId,
                TrackingNumber = trackingNumber,
            };

            await _kitRepository.AddAsync(kit);
            await _kitRepository.SaveAsync();
            return kit;
        }
        public async Task AddSampleAsync(Guid kidId, Guid personId, DateTime appointmentDate)
        {
            var entity = new Sample
            {
                SampleId = Guid.NewGuid(),
                KitId = kidId,
                CollectedDate = appointmentDate,
                ReceivedDate = appointmentDate,
                PersonId = personId
            };

            await _sampleRepository.AddAsync(entity);
            await _sampleRepository.SaveAsync();

        }
        public async Task<IEnumerable<PaymentReadDTO>> GetPaymentByUserIdAsync(Guid userId)
        {
            var payments = await _repo.GetByUserIdAsync(userId);
            return payments.Select(MapToReadDTO);
        }
    }
}