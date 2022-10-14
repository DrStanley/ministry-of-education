using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EIMS.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EIMS.Domain.Identity
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public Teacher Teacher { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
