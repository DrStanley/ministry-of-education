using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Models
{
	public class AssignSubjectStudentsViewModel
	{
        public int ExamId { get; set; }
		public int SubjectId { get; set; }
		public string SubjectName { get; set; }
		public IList<SubjectStudentViewModel> SubjectStudents { get; set; }
    }
}
