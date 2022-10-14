using System;
using System.Collections.Generic;
using System.Text;

namespace EIMS.Domain.Entities
{
    public class ExaminationsSubjects
    {
        public int ExaminationId { get; set; }
        public Examination Examination { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
