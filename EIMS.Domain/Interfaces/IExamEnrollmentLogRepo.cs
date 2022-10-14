using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EIMS.Domain.Entities;

namespace EIMS.Domain.Interfaces
{
    public interface IExamEnrollmentRepo : IGenericRepo<ExamEnrollment>
    {
        Task<bool> DoesEnrollmentExist(int examId, int schoolId);
        void CreateEnrollment(int examId, int schoolId, int totalStudents);
    }
}
