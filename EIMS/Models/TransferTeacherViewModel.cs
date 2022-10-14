using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;

namespace EIMS.Models
{
	public class TransferTeacherViewModel
	{

		public int Id { get; set; }
		public string Name { get; set; }

		public string Email { get; set; }

		[Required(ErrorMessage = "School LGA needed")]
		[Display(Name = "Current Local Government")]
		public int LocalGovernmentId { get; set; }

		public LocalGovernment LocalGovernment;

		[Required(ErrorMessage = "School needed")]
		[Display(Name = "Current School")]
		public int SchoolId { get; set; }

		public School School;

		//Transfer
		[Required(ErrorMessage = "School LGA needed")]
		[Display(Name = "Transfer Local Government")]
		public int LocalGovernmentIdTo { get; set; }

		public IEnumerable<LocalGovernment> LocalGovernmentsTo;

		[Required(ErrorMessage = "School needed")]
		[Display(Name = "Transfer School")]
		public int SchoolIdTo { get; set; }

		public IEnumerable<School> SchoolsTo;
	}
}
