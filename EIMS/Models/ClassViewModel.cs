using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;

namespace EIMS.Models
{
	public class ClassViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "ClassName field is required")]
		[Display(Name = "Class Name")]
		public string ClassName { get; set; }

		[Required(ErrorMessage = "ClassType field is required")]
		[Display(Name = "Class Type")]
		public ClassType ClassType { get; set; }


		[Required(ErrorMessage = "CreatedAt field is required")]
		[Display(Name = "Add Date")]
		public DateTime CreatedAt { get; set; }
	}
}
