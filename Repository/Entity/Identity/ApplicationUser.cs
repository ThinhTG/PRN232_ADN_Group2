using Core.Utils;
using Microsoft.AspNetCore.Identity;
using Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADN_Group2.BusinessObject.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        /// <summary>
        /// Tên người dùng
        /// </summary>
        public string? FullName { get; set; }

		public string? AvatarUrl { get; set; }
		public string? Gender { get; set; }

		public string? Address { get; set; }

        /// <summary>
        /// Ngày tạo tài khoản
        /// </summary>
        public DateTimeOffset CreatedTime { get; set; }

        /// <summary>
        /// Lần cuối cập nhật tài khoản
        /// </summary>
        public DateTimeOffset LastUpdatedTime { get; set; }


        /// <summary>
        /// Ngày xóa tài khoản (nếu chưa xóa thì 
        /// </summary>
        public DateTimeOffset? DeletedTime { get; set; }


		public int? orderCode { get; set; }



		public string? RefreshToken { get; set; }
		public DateTime? RefreshTokenExpiryTime { get; set; }

		// Thêm danh sách quan hệ với bảng UserRoles
		public virtual ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();

		public ApplicationUser()
        {
            CreatedTime = CoreHelper.SystemTimeNow;
            LastUpdatedTime = CreatedTime;
        }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
		public virtual ICollection<Blog> Blogs { get; set; }    
		public virtual ICollection<Feedback> Feedbacks { get; set; }    

	}
}
