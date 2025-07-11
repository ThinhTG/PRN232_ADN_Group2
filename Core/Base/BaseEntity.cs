﻿using Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Base
{
	public class BaseEntity
	{
		protected BaseEntity()
		{
			Id = Guid.NewGuid().ToString("N");
			CreatedTime = LastUpdatedTime = CoreHelper.SystemTimeNow;
		}

		[Key]
		public string Id { get; set; }
		public string? CreatedBy { get; set; }
		public string? LastUpdatedBy { get; set; }
		public string? DeletedBy { get; set; }
		public DateTimeOffset CreatedTime { get; set; }
		public DateTimeOffset LastUpdatedTime { get; set; }
		public DateTimeOffset? DeletedTime { get; set; }
	}
}
