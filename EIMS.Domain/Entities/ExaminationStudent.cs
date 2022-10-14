using System;
using System.Collections.Generic;
using System.Text;

namespace EIMS.Domain.Entities
{
    public class ExaminationStudent
    {
        public int StudentId { get; set; }
        public int ExaminationId { get; set; }
        public Student EnrolledStudent { get; set; }
        public Examination EnrolledExamination { get; set; }
    }
}
