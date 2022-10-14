using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;
using EIMS.Domain.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.Models
{
	public class InspectorViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "This field is required")]
		public string Title { get; set; }

		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Email is required")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Phone number is required")]
		[Display(Name = "Phone number")]
		public string PhoneNumber { get; set; }

		[Required(ErrorMessage = "Local Government is required")]
		[Display(Name = "Local Government")]
		[Remote("ValidateLGA", "Inspector",
			ErrorMessage = "LGA Is Assigned")]
		public int? LocalGovernmentId { get; set; }
		public Status Status { get; set; }

		public bool IsAssigned
		{
			get { return LocalGovernmentId != null; }
		}
		public string UserId { get; set; }

		public IEnumerable<InspectorLog>  InspectorLogs { get; set; }
		public int  SchoolsCount { get; set; }
		public AppUser User { get; set; }

		public LocalGovernment LocalGovernment { get; set; }
	}
}
