using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using EIMS.Domain.Entities.Enum;
using EIMS.Domain.Identity;

namespace EIMS.Domain.Entities
{
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? Dob { get; set; }
        public string Address { get; set; }
        public States StateOfOrigin { get; set; }
        public int? SchoolId { get; set; }
        public Gender? Gender { get; set; }
        public School School { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public Teacher Teacher { get; set; }
    }
}
