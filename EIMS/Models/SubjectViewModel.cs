using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;

namespace EIMS.Models
{
	public class SubjectViewModel
	{
		
		public int Id { get; set; }
		[Required(ErrorMessage = "ClassName field is required")]
		[Display(Name = "Subject Name")]
		public string SubjectName { get; set; }

		[Required(ErrorMessage = "ClassName field is required")]
		[Display(Name = "Subject Level")]
		public SchoolCategory LevelCategory { get; set; }

		public bool IsAdded { get; set; }
	}
}
