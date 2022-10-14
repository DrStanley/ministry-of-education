using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EIMS.Models
{
    public class AddSubjectsViewModel
    {
        [Required]
        public int ExamId { get; set; }
        public string ExaminationName { get; set; }
        public IList<SubjectViewModel> Subjects { get; set; }

    }
}
