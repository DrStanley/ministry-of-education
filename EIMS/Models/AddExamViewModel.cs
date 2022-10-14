using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;

namespace EIMS.Models
{
    public class AddExamViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter the examination name")]
        [Display(Name = "Examination Name")]
        [MaxLength(50, ErrorMessage = "Maximum of 30 characters")]
        public string ExaminationName { get; set; }

        [Required(ErrorMessage = "Please enter the examination code")]
        [Display(Name = "Examination Code")]
        [MaxLength(10, ErrorMessage = "Maximum of 10 characters")]
        public string ExaminationCode { get; set; }

        [Required(ErrorMessage = "Please select a class")]
        [Display(Name = "Examination Class")]
        public int ClassId { get; set; }
        public IEnumerable<Classes> Classes { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [Display(Name = "Exam Category")]
        public SchoolCategory ExamCategory { get; set; }
    }
}
