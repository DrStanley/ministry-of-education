using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EIMS.Domain.Entities.Enum;

namespace EIMS.Models
{
    public class QualificationViewModel
    {
        public int Id { get; set; }
        [Required]
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [Display(Name = "School name")]
        public string SchoolName { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [Display(Name = "Level of Education")]
        public EducationLevel EducationLevel { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [Display(Name = "Start year")]
        public int StartYear { get; set; } = DateTime.Today.Year;

        [Required(ErrorMessage = "Field is required")]
        [Display(Name = "Completion year")]
        public int EndYear { get; set; } = DateTime.Today.Year;

        public IEnumerable<int> YearList = (from n in Enumerable.Range(0, 50)
            select DateTime.Now.Year - n);
    }
}
