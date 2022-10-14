using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;

namespace EIMS.Models
{
	public class EnrollSubjectViewModel
	{

		public int Id { get; set; }
		[Required(ErrorMessage = "ClassName field is required")]
		[Display(Name = "Subject Name")]
		public string SubjectName { get; set; }

		[Required(ErrorMessage = "ClassName field is required")]
		[Display(Name = "Subject Level")]
		public SchoolCategory LevelCategory { get; set; }
		public int OfferedStudents { get; set; }
		public int ExamId { get; set; }
		public bool IsEnrolled { get; set; }
	}
}