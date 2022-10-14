using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using EIMS.Domain.Entities.Enum;

namespace EIMS.Domain.Entities
{
    /// <summary>
    /// When the school paid
    /// </summary>
    public class ExamEnrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ExamId { get; set; }
        public Examination Examination { get; set; }
        public EnrollmentStatus EnrollmentStatus { get; set; }
        public int TotalStudentsEnrolled { get; set; }  
        public int SchoolId { get; set; }
        public School School { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
