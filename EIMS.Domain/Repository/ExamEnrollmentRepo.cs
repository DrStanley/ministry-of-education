using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EIMS.Domain.Data;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;
using EIMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Domain.Repository
{
    public class ExamEnrollmentRepo : GenericRepo<ExamEnrollment>, IExamEnrollmentRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public ExamEnrollmentRepo(ApplicationDbContext context)
        :base(context)
        {
            _dbContext = context;
        }

        public void CreateEnrollment(int examId, int schoolId, int totalStudents)
        {
            var enrollment = new ExamEnrollment()
            {
                ExamId = examId,
                SchoolId = schoolId,
                EnrollmentStatus = EnrollmentStatus.Pending,
                TotalStudentsEnrolled = totalStudents
            };
            Insert(enrollment);
        }

        public async Task<bool> DoesEnrollmentExist(int examId, int schoolId)
        {
            return (await _dbContext.ExamEnrollments.FirstOrDefaultAsync(e =>
                       e.ExamId == examId && e.SchoolId == schoolId)) !=
                   null;
        }
    }
}
