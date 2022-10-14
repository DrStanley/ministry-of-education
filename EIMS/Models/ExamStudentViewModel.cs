using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Models
{
    public class ExamStudentViewModel
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public int ClassesId { get; set; }
        public bool IsAdded { get; set; }
    }
}
