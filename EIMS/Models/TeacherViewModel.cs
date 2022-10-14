using System.Collections.Generic;
using AutoMapper;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;
using EIMS.Domain.Identity;

namespace EIMS.Models
{
    public class TeacherViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public Status Status { get; set; }
        public AppUser User { get; set; }

        public string Name
        {
            get { return User.Name; }
        }

        public string Email
        {
            get { return User.Email; }
        }
        public int? SchoolId { get; set; }
        public School School { get; set; }
        public int? LocalGovernmentId { get; set; }

        public bool IsAssigned
        {
            get { return  LocalGovernmentId != null; }
        }

        public LocalGovernment LocalGovernment { get; set; }
        public IEnumerable<TeacherLog> TeacherLogs { get; set; }
    }
}
