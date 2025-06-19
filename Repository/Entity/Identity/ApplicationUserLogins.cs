using Core.Utils;
using Microsoft.AspNetCore.Identity;

namespace ADN_Group2.BusinessObject.Identity
{
	public class ApplicationUserLogins : IdentityUserLogin<Guid>
    {
        public DateTimeOffset CreatedTime { get; set; }

        public DateTimeOffset LastUpdatedTime { get; set; }

        public DateTimeOffset? DeletedTime { get; set; }
        public ApplicationUserLogins()
        {
            CreatedTime = CoreHelper.SystemTimeNow;
            LastUpdatedTime = CreatedTime;
        }
    }
}
