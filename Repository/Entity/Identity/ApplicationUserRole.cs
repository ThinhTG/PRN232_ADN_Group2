using Core.Utils;
using Microsoft.AspNetCore.Identity;
using System;

namespace ADN_Group2.BusinessObject.Identity
{
	public class ApplicationUserRole : IdentityUserRole<Guid>
    {
        public DateTimeOffset CreatedTime { get; set; }

        public DateTimeOffset LastUpdatedTime { get; set; }

        public DateTimeOffset? DeletedTime { get; set; }
        public ApplicationUserRole()
        {
            CreatedTime = CoreHelper.SystemTimeNow;
            LastUpdatedTime = CreatedTime;
        }

		public virtual ApplicationUser User { get; set; }

		public virtual ApplicationRole Role { get; set; }
	}
}
