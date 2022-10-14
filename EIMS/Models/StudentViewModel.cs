using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;

namespace EIMS.Models
{
	public class StudentViewModel
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public string StateOfOrigin { get; set; }

		public Gender Gender { get; set; }

		public DateTime DateOfBirth { get; set; }

		public string NextOfKinName { get; set; }

		public string NextOfKinRelation { get; set; }

		public string NextOfContact { get; set; }

		public int? LocalGovernmentId { get; set; }
		public LocalGovernment LocalGovernment { get; set; }
		public int? ClassesId { get; set; }
		public Classes Classes { get; set; }
		public ClassType ClassType { get; set; }
		public int? SchoolId { get; set; }
		public School School { get; set; }
	}
}
