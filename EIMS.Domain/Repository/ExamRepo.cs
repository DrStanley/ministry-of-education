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
    public class ExamRepo : GenericRepo<Examination>, IExamRepo
    {
        private readonly ApplicationDbContext _dbContext;

        public ExamRepo(ApplicationDbContext context)
        : base(context)
        {
            _dbContext = context;
        }

        public void ActivateOrDeactivateExam(int id, Status action)
        {
            var exam = GetById(id);
            if (exam != null)
            {
                switch (action)
                {
                    case Status.Active:
                        exam.ExamStatus = Status.Active;
                        break;
                    case Status.Inactive:
                        exam.ExamStatus = Status.Inactive;
                        break;
                }
                Update(exam);
            }
            else
            {
                throw new Exception("Exam cannot be found");
            }
        }

        public async Task<Examination> GetExam(int id)
        {
            return await _dbContext.Examinations.Include(e=>e.ExaminationsSubjects).FirstAsync(e=>e.Id==id);
        }

        public async Task<Examination> GetExamIncludeStudent(int id)
        {
            return await _dbContext.Examinations.Include(e => e.ExaminationStudents).FirstAsync(e => e.Id == id);
        }
    }
}
