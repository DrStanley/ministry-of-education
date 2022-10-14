using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;

namespace EIMS.Models
{
	public class CreateStudentViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }

		[Required(ErrorMessage = "State Of Origin needed")]
		[Display(Name = "State Of Origin")]
		public States StateOfOrigin { get; set; }

		[Required(ErrorMessage = "Gender needed")]
		[Display(Name = "Gender")]
		public Gender Gender { get; set; }

		[Required(ErrorMessage = "Date Of Birth needed")]
		[Display(Name = "Date Of Birth")]
		public DateTime DateOfBirth { get; set; }

		[Required(ErrorMessage = "Next Of Kin Name needed")]
		[Display(Name = "Next Of Kin Name")]
		public string NextOfKinName { get; set; }

		[Required(ErrorMessage = "Next Of Kin Relation needed")]
		[Display(Name = "Next Of Kin Relation")] 
		public string NextOfKinRelation { get; set; }

		[Required(ErrorMessage = "Next Of kin Contact needed")]
		[Display(Name = "Next Of kin Contact")] 
		public string NextOfContact { get; set; }

		[Display(Name = "School")]
		public int? SchoolId { get; set; }

		[Display(Name = "Class Type")]
		public ClassType? ClassType { get; set; }

		[Display(Name = "Local Government")]
		public int? LocalGovernmentId { get; set; }

		[Display(Name = "Class")]
		public int? ClassesId { get; set; }
	}
}
