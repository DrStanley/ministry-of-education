using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Models
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        [Display(Name="Current Password")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name="New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name="Confirm Password")]
        [Compare("NewPassword", ErrorMessage = "Passwords have to match")]
        public string ConfirmPassword { get; set; }
    }
}
