using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;
using EIMS.Models;

namespace EIMS.Infrastructure.Interfaces
{
    public interface IExamService
    {
        ExamViewModel GetExam(int examId);
        AddExamViewModel InitExamViewModel();
        int AddExam(AddExamViewModel exam);
        int UpdateExam(ExamViewModel exam);
        IEnumerable<ExamViewModel> GetAllExams(string status = null);
        Examination DeleteExam(int examId);
        int ActivateOrDeactivateExam(int examId, Status status);
        Task<AddSubjectsViewModel> GetExamSubjects(int examId);
        Task<string> AddSubjectsToExam(AddSubjectsViewModel addSubjectsViewModel);
    }
}
