using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Models
{
    public class WorkExperienceViewModel
    {
        public int Id { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Job title")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Enter a start date")]
        [Display(Name = "Start date")]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Display(Name = "End date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Still work here?")] 
        public bool StillWorking { get; set; } = false;

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.MultilineText)]
        [Display(Name="Job summary")]
        public string JobSummary { get; set; }
    }
}
