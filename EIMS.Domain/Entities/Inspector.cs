using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using EIMS.Domain.Entities.Enum;
using EIMS.Domain.Identity;

namespace EIMS.Domain.Entities
{
	public class Inspector
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string UserId { get; set; }
		public AppUser User { get; set; }
		public int? LocalGovernmentId { get; set; }
		public LocalGovernment LocalGovernment { get; set; }
		public Status Status { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
