using ADN_Group2.BusinessObject.Identity;
using System;

namespace Repository.Entity
{
    public class Address
    {
        public Guid AddressId { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
} 