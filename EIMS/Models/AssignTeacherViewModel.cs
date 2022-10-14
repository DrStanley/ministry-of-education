using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;

namespace EIMS.Models
{
	public class AssignTeacherViewModel
	{
		
		public int Id { get; set; }
		public string Name { get; set; }

		public string Email { get; set; }

		[Required(ErrorMessage = "School LGA needed")]
		[Display(Name = "Local Government")]
		public int LocalGovernmentId { get; set; }

		public IEnumerable<LocalGovernment> LocalGovernments;
		
		[Required(ErrorMessage = "School needed")]
		[Display(Name = "Schools")]
		public int SchoolId { get; set; }

		public IEnumerable<School> Schools;
	}
}
