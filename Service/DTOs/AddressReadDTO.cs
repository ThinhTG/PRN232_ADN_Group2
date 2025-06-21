using System;

namespace Service.DTOs
{
    public class AddressReadDTO
    {
        public Guid AddressId { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public Guid UserId { get; set; }
    }
} 