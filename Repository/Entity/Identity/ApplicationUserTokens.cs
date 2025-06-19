using Core.Utils;
using Microsoft.AspNetCore.Identity;

namespace ADN_Group2.BusinessObject.Identity
{
	public class ApplicationUserTokens : IdentityUserToken<Guid>
    {
        public DateTimeOffset CreatedTime { get; set; }

        public DateTimeOffset LastUpdatedTime { get; set; }

        public DateTimeOffset? DeletedTime { get; set; }
        public ApplicationUserTokens()
        {
            CreatedTime = CoreHelper.SystemTimeNow;
            LastUpdatedTime = CreatedTime;
        }
    }
}
