using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;

namespace EIMS.Models
{
    public class ExamViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "This field cannot be empty")] 
        [Display(Name = "Examination name")]
        public string ExaminationName { get; set; }
        [Required(ErrorMessage = "This field cannot be empty")]
        [Display(Name = "Examination code")]
        public string ExaminationCode { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Examination category")]
        public SchoolCategory ExamCategory { get; set; }

        [Required(ErrorMessage = "This field cannot be empty")]
        [Display(Name = "Examination class")]
        public int ClassId { get; set; }

        public IEnumerable<Classes> Classes { get; set; }

        [Display(Name = "Fee per student")]
        public decimal ExamFee { get; set; }

        [Display(Name = "Examination year")]
        public int ExamYear { get; set; } = DateTime.Now.Year;

        public IEnumerable<int> YearList = (from n in Enumerable.Range(0, 10)
            select DateTime.Now.Year + n);

        public string Fee => ExamFee > 0 ? $"N{ExamFee}" : "Not Assigned";

        public string Category
        {
            get
            {
                switch (ExamCategory)
                {
                    case SchoolCategory.PRIMARY:
                        return "Primary";
                    case SchoolCategory.SECONDARY:
                        return "Secondary";
                    default:
                        return "Not defined";
                }
            }
        }
    }
}
