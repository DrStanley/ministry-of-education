using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using EIMS.Domain.Entities.Enum;
using EIMS.Domain.Identity;

namespace EIMS.Domain.Entities
{
	public class Student
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Name{ get; set; }
		public States StateOfOrigin{ get; set; }
		public Gender Gender { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string NextOfKinName{ get; set; }
		public string NextOfKinRelation{ get; set; }
		public string NextOfContact{ get; set; }
		public int? SchoolId { get; set; }
		public School School { get; set; }
		public ClassType? ClassType { get; set; }
		public int? LocalGovernmentId { get; set; }
		public LocalGovernment LocalGovernment { get; set; }
		public int? ClassesId { get; set; }
		public Classes Classes { get; set; }
		public ICollection<OfferedStudentExamSubject> OfferedStudentExamSubjects { get; set; }
		public ICollection<ExaminationStudent> StudentExaminations { get; set; }
		public DateTime CreatedAt { get; set; }

    }
}
