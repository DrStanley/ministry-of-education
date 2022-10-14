using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.Models
{
	public class CreateInspectorViewModel
	{
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
			ErrorMessage = "LGA has Inspector", HttpMethod = "POST")]
		public int LocalGovernmentId { get; set; }

		public IEnumerable<LocalGovernment> LocalGovernments { get; set; }

	}
}
