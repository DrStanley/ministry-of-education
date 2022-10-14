using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;

namespace EIMS.Models
{
	public class AdminViewModel
	{
		public IEnumerable<SchoolViewModel> Schools { get; set; }
		public IEnumerable<TeacherViewModel> Teachers { get; set; }
		public IEnumerable<LocalGovernment> LocalGovernments { get; set; }
		public IEnumerable<StudentViewModel> Students { get; set; }

	}
}
