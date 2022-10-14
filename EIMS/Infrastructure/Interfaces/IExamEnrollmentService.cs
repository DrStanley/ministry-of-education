using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities.Enum;
using EIMS.Models;

namespace EIMS.Infrastructure.Interfaces
{
    public interface IExamEnrollmentService
    {
        IEnumerable<ExamViewModel> GetAccreditedExams(string adminEmail);
        Task<AddExamStudentsViewModel> GetExamExamStudents(int examId);
        Task<Tuple<int, string>> EnrollStudentsForExam(int examId, AddExamStudentsViewModel examStudentsVm);
        Task<IEnumerable<EnrollSubjectViewModel>> GetExamSubjects(int examId);
        AssignSubjectStudentsViewModel GetSubjectStudents(int examId, int subjectId);
        Tuple<int, string> AssignSubjectCandidates(AssignSubjectStudentsViewModel assignViewModel);
    }
}
