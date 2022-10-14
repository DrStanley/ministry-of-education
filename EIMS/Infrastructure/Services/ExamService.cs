using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;
using EIMS.Domain.Interfaces;
using EIMS.Infrastructure.Interfaces;
using EIMS.Models;

namespace EIMS.Infrastructure.Services
{
    public class ExamService: IExamService
    {
        private readonly IExamRepo _examRepo;
        private readonly ISubjectRepo _subjectRepo;
        private readonly IClassesRepo _classesRepo;
        private readonly IMapper _mapper;

        public ExamService(
            IExamRepo examRepo,
            ISubjectRepo subjectRepo,
            IClassesRepo classesRepo,
            IMapper mapper
            )
        {
            _examRepo = examRepo;
            _subjectRepo = subjectRepo;
            _classesRepo = classesRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds a new examination to the database
        /// </summary>
        /// <param name="exam"></param>
        /// <returns></returns>
        public int AddExam(AddExamViewModel exam)
        {
            var examination = _mapper.Map<Examination>(exam);
            _examRepo.Insert(examination);
            return _examRepo.Save();
        }

        /// <summary>
        /// Updates the details of an exam
        /// </summary>
        /// <param name="exam"></param>
        /// <returns></returns>
        public int UpdateExam(ExamViewModel exam)
        {
            var examination = _mapper.Map<Examination>(exam);
            _examRepo.Update(examination);
            return _examRepo.Save();
        }

        /// <summary>
        /// Deletes an examination from the database
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        public Examination DeleteExam(int examId)
        {
            var exam = _examRepo.GetById(examId);
            if (exam == null)
            {
                throw new Exception("Exam does not exist in the database");
            }
            _examRepo.Delete(examId);
            var result = _examRepo.Save();
            if (result < 1)
            {
                throw new Exception($"Failed to delete exam");
            }

            return exam;
        }

        /// <summary>
        /// Returns all the examination
        /// </summary>
        /// <param name="status">Not required. Returns exams based on their status</param>
        /// <returns></returns>
        public IEnumerable<ExamViewModel> GetAllExams(string status = null)
        {
            switch (status)
            {
                case "Active":
                    return _mapper.Map<IEnumerable<ExamViewModel>>(_examRepo.Find(ex=>ex.ExamStatus==Status.Active));
                case "Inactive":
                    return _mapper.Map<IEnumerable<ExamViewModel>>(_examRepo.Find(ex => ex.ExamStatus == Status.Inactive));
            }
            return _mapper.Map<IEnumerable<ExamViewModel>>(_examRepo.GetAll());
        }

        /// <summary>
        /// Returns the target examination
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        public ExamViewModel GetExam(int examId)
        { 
            var exam = _examRepo.GetById(examId);
            if (exam == null)
            {
                throw new Exception("Exam was not found");
            }
            
            var examVm= _mapper.Map<ExamViewModel>(exam);
            examVm.Classes = _classesRepo.GetAll();
            return examVm;
        }

        /// <summary>
        /// Activates or deactivates the exam
        /// </summary>
        /// <param name="examId">The id of the target examination</param>
        /// <param name="status">The state; active to activate and inactive to deactivate</param>
        /// <returns></returns>
        public int ActivateOrDeactivateExam(int examId, Status status)
        {
            var exam = _examRepo.GetById(examId);
            if (exam == null)
            {
                throw new Exception("Exam was not found");
            }

            switch (status)
            {
                case Status.Active:
                    exam.ExamStatus = Status.Active;
                    break;
                case Status.Inactive:
                    exam.ExamStatus = Status.Inactive;
                    break;
            }
            _examRepo.Update(exam);
            var result = _examRepo.Save();
            var action = status == Status.Active ? "activate" : "deactivate";
            if (result < 1)
            {
                throw new Exception($"Failed to {action} exam");
            }

            return result;
        }

        /// <summary>
        /// Gets all subjects that belong to the same school category as the exam
        /// If a subject is in a many to many relationship with the exam it will be mark as added
        /// </summary>
        /// <param name="examId"></param>
        /// <returns></returns>
        public async Task<AddSubjectsViewModel> GetExamSubjects(int examId)
        {
            var exam = await _examRepo.GetExam(examId);
            var scopedSubjects = _subjectRepo.Find(s =>
                s.LevelCategory == exam.ExamCategory || s.LevelCategory == SchoolCategory.COMBINED);
            var subjects = _mapper.Map<IList<SubjectViewModel>>(scopedSubjects);

            foreach (var subject in subjects)
            {
                // Checks if the subject is already added to the exam
                subject.IsAdded = exam.ExaminationsSubjects.SingleOrDefault(es => es.SubjectId == subject.Id) != null;
            }

            var addSubjectVm = new AddSubjectsViewModel()
            {
                ExamId = examId,
                ExaminationName = exam.ExaminationName,
                Subjects = subjects
            };
            return addSubjectVm;
        }

        /// <summary>
        /// Adds subjects to the target examination
        /// </summary>
        /// <param name="addSubjectsViewModel"></param>
        /// <returns></returns>
        public async Task<string> AddSubjectsToExam(AddSubjectsViewModel addSubjectsViewModel)
        {
            try
            {
                var exam = await _examRepo.GetExam(addSubjectsViewModel.ExamId);
                foreach (var subject in addSubjectsViewModel.Subjects)
                {
                    // Adds the subject to the examination
                    if (subject.IsAdded &&
                        exam.ExaminationsSubjects.SingleOrDefault(es => es.SubjectId == subject.Id) == null)
                    {
                        exam.ExaminationsSubjects.Add(new ExaminationsSubjects()
                        {
                            SubjectId = subject.Id,
                        });
                    }
                    // Remove the subject from the examination
                    else if (!subject.IsAdded &&
                             exam.ExaminationsSubjects.SingleOrDefault(es => es.SubjectId == subject.Id) != null)
                    {
                        var examSubject = exam.ExaminationsSubjects.First(es => es.SubjectId == subject.Id);
                        exam.ExaminationsSubjects.Remove(examSubject);
                    }
                }

                var result = _examRepo.Save();
                if (result < 1)
                {
                    throw new Exception("Failed to add subjects");
                }
                return "Subjects successfully added to examination";
            }
            catch
            {
                throw new Exception("Failed to add subjects");
            }


        }

        public AddExamViewModel InitExamViewModel()
        {
            return new AddExamViewModel(){Classes = _classesRepo.GetAll()};
        }
    }
}