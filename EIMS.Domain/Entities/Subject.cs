using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EIMS.Domain.Entities.Enum;

namespace EIMS.Domain.Entities
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SubjectName { get; set; }
        public ICollection<TeachersSubjects> TeachersSubjects { get; set; }
        public SchoolCategory LevelCategory { get; set; }
        public ICollection<SubjectClasses> SubjectsClasses { get; set; }
        public ICollection<ExaminationsSubjects> SubjectsExaminations { get; set; }
        public ICollection<OfferedStudentExamSubject> OfferedExamStudents { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
