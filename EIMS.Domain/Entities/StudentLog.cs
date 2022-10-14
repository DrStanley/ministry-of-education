using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using EIMS.Domain.Entities.Enum;

namespace EIMS.Domain.Entities
{
	public class StudentLog
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public int StudentId { get; set; }
		public Student Student { get; set; }
		public int? SchoolId { get; set; }
		public School School { get; set; }
		public ClassType ClassType { get; set; }
		public int? ClassesId { get; set; }
		public Classes Classes { get; set; }
		public string Details { get; set; }
		public LogType LogType { get; set; }
		public int? LocalGovernmentId { get; set; }
		public LocalGovernment LocalGovernment { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
