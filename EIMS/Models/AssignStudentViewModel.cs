using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;

namespace EIMS.Models
{
	public class AssignStudentViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Student Name field is required")]
		[Display(Name = "Student Name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Local Government field is required")]
		[Display(Name = "Local Government")]
		public int LocalGovernmentId { get; set; }
		public LocalGovernment LocalGovernment { get; set; }

		[Required(ErrorMessage = "Class field is required")]
		[Display(Name = "Class")]
		public int ClassesId { get; set; }
		public Classes Classes { get; set; }

		[Required(ErrorMessage = "Class Type field is required")]
		[Display(Name = "Class Type")]
		public ClassType ClassType { get; set; }

		[Required(ErrorMessage = "School field is required")]
		[Display(Name = "School")]
		public int SchoolId { get; set; }
		public School School { get; set; }


		public IEnumerable<LocalGovernment> LocalGovernments { get; set; }
		public IEnumerable<Classes> Classeses { get; set; }
	}
}
