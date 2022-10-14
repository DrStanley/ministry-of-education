using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using EIMS.Domain.Entities.Enum;

namespace EIMS.Domain.Entities
{
	public class Classes
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string ClassName { get; set; }
		public ClassType ClassType { get; set; }
		public ICollection<SubjectClasses> SubjectsClasses { get; set; }
		public DateTime CreatedAt { get; set; }

	}
}
