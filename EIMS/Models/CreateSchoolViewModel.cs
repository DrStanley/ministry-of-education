using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;

namespace EIMS.Models
{
    public class CreateSchoolViewModel
    {
        [Required(ErrorMessage = "School name is required")]
        [Display(Name = "School Name")]
        public string SchoolName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "School Email needed")]
        public string Email { get; set; }

        [Required(ErrorMessage = "School Code needed")]
        [Display(Name = "School Code")]
        public string SchoolCode { get; set; }

        [Required(ErrorMessage = "School Mobile Number needed")]
        [Display(Name="School Mobile Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "School Address needed")]
        public string Address { get; set; }

        [Required(ErrorMessage = "School LGA needed")]
        [Display(Name = "Local Government")]
        public int LocalGovernmentId { get; set; }

        public IEnumerable<LocalGovernment> LocalGovernments;

        [Required(ErrorMessage = "School Date Of Incorporation needed")]
        [Display(Name = "Incoporation Date")]
        public DateTime IncorporationDate { get; set; }

        [Required(ErrorMessage = "Title needed")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Principal Name needed")]
        [MinLength(5, ErrorMessage = "Principal Name must be at least 5 characters long")]
        [Display(Name = "Principal Name")]
        public string PrincipalName { get; set; }

        [Required(ErrorMessage = "School Category needed")]
        [Display(Name = "School Category")]
        public SchoolCategory SchoolCategory { get; set; }

        [Required(ErrorMessage = "School Type needed")]
        [Display(Name = "School Type")]
        public SchoolType SchoolType { get; set; }
	}
}
