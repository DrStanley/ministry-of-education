using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.Models
{
	public class TransferInspectorViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }

		[Display(Name = "Current LGA")]
		public int? LocalGovernmentId { get; set; }
		public LocalGovernment LocalGovernment { get; set; }
		
		[Required(ErrorMessage = "Local Government is required")]
		[Display(Name = "Transfer LGA")]
		[Remote("ValidateLGA", "Inspector",
			ErrorMessage = "LGA has Inspector")]
		public int LocalGovernmentIdTo { get; set; }
	}
}
