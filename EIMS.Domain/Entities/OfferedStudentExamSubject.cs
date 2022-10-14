using System;
using System.Collections.Generic;
using System.Text;

namespace EIMS.Domain.Entities
{
    public class OfferedStudentExamSubject
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int ExamId { get; set; }
        public Examination Examination { get; set; }
        public int SubjectId { get; set; }
        public Subject OfferedSubject { get; set; }
    }
}
