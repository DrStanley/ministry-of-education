using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;
using EIMS.Domain.Identity;

namespace EIMS.Models
{
    public class ProfileViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Date of birth")]
        public DateTime Dob { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.Password)]
        public string Address { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.Text)]
        [Display(Name = "State of Origin")]
        public States StateOfOrigin { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        public Gender Gender { get; set; }

        public string UpdateType { get; set; }

        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }

        public AppUser User { get; set; }
        
        public string UserId { get; set; }
    }
}
