using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;
using EIMS.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Domain.Data
{
	public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        } 
        public DbSet<School> Schools { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<TeachersSubjects> TeachersSubjects { get; set; }

        public DbSet<LocalGovernment> LocalGovernments { get; set; }

        public DbSet<UserProfile> Profiles { get; set; }
        
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<TeacherLog> TeacherLogs { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<SubjectClasses> SubjectClasses { get; set; }
        public DbSet<StudentLog> StudentLogs { get; set; }
        public DbSet<Inspector> Inspectors { get; set; }
        public DbSet<InspectorLog>  InspectorLogs { get; set; }
        public DbSet<OfferedStudentExamSubject> OfferedStudentExamSubjects { get; set; }
        public DbSet<ExamEnrollment> ExamEnrollments { get; set; }
        public DbSet<ExaminationStudent> ExaminationStudents { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.GenerateTables();
            builder.ConfigureEntities();
            builder.ExamSettings();
            builder.ExamEnrollmentSettings();
            builder.SeedData();
            builder.SeedLGAs();
            //  builder.Entity<School>().ToTable("School");
        }

    }
}
