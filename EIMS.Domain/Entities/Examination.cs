using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using EIMS.Domain.Entities.Enum;

namespace EIMS.Domain.Entities
{
    public class Examination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ExaminationName { get; set; }
        public string ExaminationCode { get; set; }
        public SchoolCategory ExamCategory { get; set; }
        public int ClassId { get; set; }
        public Classes ExamClass { get; set; }
        public ICollection<ExaminationsSubjects> ExaminationsSubjects { get; set; }
        public virtual ICollection<ExaminationStudent> ExaminationStudents { get; set; }
        public decimal? ExamFee { get; set; }
        public int? ExamYear { get; set; }
        public Status ExamStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
