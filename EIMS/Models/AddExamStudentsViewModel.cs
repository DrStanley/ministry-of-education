using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EIMS.Models
{
    public class AddExamStudentsViewModel
    {
        [Editable(false)]
        public int ExamId { get; set; }
        [Required]
        public int SchoolId { get; set; }
        public string ExaminationName { get; set; }
        public IList<ExamStudentViewModel> ExamStudent { get; set; }

    }
}
