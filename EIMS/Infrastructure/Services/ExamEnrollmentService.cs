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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;

namespace EIMS.Infrastructure.Services
{
    [Authorize("SchoolAdmin")]
    public class ExamEnrollmentService : IExamEnrollmentService
    {
        private readonly IExamEnrollmentRepo _examEnrollmentRepo;
        private readonly IStudentRepo _studentRepo;
        private readonly ISchoolRepo _schoolRepo;
		private readonly ISubjectRepo _subjectRepo;
		private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IExamRepo _examRepo;
        private readonly IMapper _mapper;

        public ExamEnrollmentService(
            IExamEnrollmentRepo examEnrollmentRepo,
            IExamRepo examRepo,
            ISchoolRepo schoolRepo, 
            ISubjectRepo subjectRepo,
            IStudentRepo studentRepo,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _examEnrollmentRepo = examEnrollmentRepo;
            _examRepo = examRepo;
            _studentRepo = studentRepo;
            _schoolRepo = schoolRepo;
            _subjectRepo = subjectRepo;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private enum Action
        {
            Add,
            Remove
        }

        public async Task<AddExamStudentsViewModel> GetExamExamStudents(int examId)
        {
            var exam = await _examRepo.GetExamIncludeStudent(examId);
            var addExamViewModel = _mapper.Map<AddExamStudentsViewModel>(exam);
            
            var school = _schoolRepo.FindSchoolEmail(_httpContextAccessor.HttpContext.User.Identity.Name);
            addExamViewModel.SchoolId = school.Id;

            var schoolStudents = _studentRepo.Find(st => st.SchoolId == school.Id);
            if (schoolStudents == null)
            {
                throw new Exception("There are no registered students for your school");
            }

            var eligibleStudents = _mapper.Map<IList<ExamStudentViewModel>>( schoolStudents.Where(st => st.ClassesId == exam.ClassId));
            if (!eligibleStudents.Any())
            {
                throw new Exception("No student in your school is eligible to write this exam");
            }

            foreach (var eligibleStudent in eligibleStudents)
            {
                eligibleStudent.IsAdded =
                    exam.ExaminationStudents.SingleOrDefault(exSt => exSt.StudentId == eligibleStudent.Id) != null;
            }

            addExamViewModel.ExamStudent = eligibleStudents;

            return addExamViewModel;
        }

        public async Task<Tuple<int, string>> EnrollStudentsForExam(int examId, AddExamStudentsViewModel examStudentsVm)
        {
            try
            {
                var exam = await _examRepo.GetExamIncludeStudent(examId);
                var transaction = _examRepo.GetDbContextTransaction();
                foreach (var student in examStudentsVm.ExamStudent)
                {
                    // Add new student to the examination
                    if (student.IsAdded &&
                        !exam.ExaminationStudents.Any(es => es.StudentId == student.Id))
                    {
                        EnrollOrUnEnrollStudent(exam, student.Id, Action.Add);
                    }

                    // Remove the student from the examination
                    else if (!student.IsAdded &&
                             exam.ExaminationStudents.Any(es => es.StudentId == student.Id))
                    {
                        EnrollOrUnEnrollStudent(exam, student.Id, Action.Remove);
                    }
                }

                var result = _examRepo.Save();
                var totalStudentEnrolled = exam.ExaminationStudents.Count();
                if(result > 0)
                {
                    // check if school has enrolled for exam
                    if (await _examEnrollmentRepo.DoesEnrollmentExist(examId, examStudentsVm.SchoolId))
                    {
                        await transaction.CommitAsync();
                        return new Tuple<int, string>(result, "Successfully enrolled students for exam");
                    }

                    // Create new enrollment
                    _examEnrollmentRepo.CreateEnrollment(examId, examStudentsVm.SchoolId, totalStudentEnrolled);
                    var saveCount = _examEnrollmentRepo.Save();
                    if (saveCount <= 0)
                    {
                        await transaction.RollbackAsync();
                        return new Tuple<int, string>(0, "Enrollment failed");
                    }
                    await transaction.CommitAsync();
                    return new Tuple<int, string>(saveCount, 
                        $"You have enrolled {examStudentsVm.ExamStudent.Count(es=>es.IsAdded)} students for {examStudentsVm.ExaminationName}");
                }

                await transaction.RollbackAsync();
                return new Tuple<int, string>(1, "No changes were made.");

            }
            catch(Exception error)
            {
                return new Tuple<int, string>(0, error.Message);
            }

        }
        
        /// <summary>
        /// Gets active examinations that can be taken by the school
        /// </summary>
        /// <param name="adminEmail"></param>
        /// <returns></returns>
        public IEnumerable<ExamViewModel> GetAccreditedExams(string adminEmail)
        {
            var schoolCategory = _schoolRepo.FindSchoolEmail(adminEmail).SchoolCategory;
            //Get active exams in school category
            if (schoolCategory == SchoolCategory.COMBINED)
            {
                return _mapper.Map<IEnumerable<ExamViewModel>>(_examRepo.Find(ex => ex.ExamStatus == Status.Active).ToList());
            }
            return _mapper.Map<IEnumerable<ExamViewModel>>(_examRepo.Find(ex =>ex.ExamCategory==schoolCategory
                                                                               && ex.ExamStatus== Status.Active).ToList());
        }

		public async Task<IEnumerable<EnrollSubjectViewModel>> GetExamSubjects(int examId)
		{
            var exam = await _examRepo.GetExam(examId);
			var subjects = _subjectRepo.GetSubjectsIncludeEnrolled();
            var schoolStudents = (await _schoolRepo.FindSchoolIncludeStudents(_httpContextAccessor.HttpContext.User.Identity.Name)).Students;
			var enrollSubjectsList = new List<EnrollSubjectViewModel>();
			foreach (var sub
				in subjects)
			{
				if (exam.ExaminationsSubjects.Any(es => es.SubjectId == sub.Id))
				{
					var enrollSubjectsVm = _mapper.Map<EnrollSubjectViewModel>(sub);
					enrollSubjectsVm.ExamId = examId;
                    if(sub.OfferedExamStudents.Any(ofs=> ofs.ExamId == examId))
                    {
                        var allEnrolledStudents = sub.OfferedExamStudents.Where(ofs => ofs.ExamId == examId);
                        foreach (var student in allEnrolledStudents)
                        {
                            if(schoolStudents.Any(s=>s.Id == student.StudentId))
                            {
                                enrollSubjectsVm.OfferedStudents += 1;
                            }
                        }
                    }
                    enrollSubjectsList.Add(enrollSubjectsVm);
				}
			}
			return enrollSubjectsList;
		}

		public AssignSubjectStudentsViewModel GetSubjectStudents(int examId, int subjectId)
		{
			var school = _schoolRepo.FindSchoolEmail(_httpContextAccessor.HttpContext.User.Identity.Name);

			var enrollStudents = _studentRepo.FindByInclude(s => s.SchoolId == school.Id, s => s.StudentExaminations)
				.Where(s=>s.StudentExaminations.Any(se=>se.ExaminationId==examId));

			var subject = _subjectRepo.FindOneInclude(s => s.Id == subjectId, s => s.OfferedExamStudents);
			
			var studentList = _mapper.Map<IList<SubjectStudentViewModel>>(enrollStudents.ToList());
			var assignStudentVm = new AssignSubjectStudentsViewModel()
            {
                ExamId = examId,
                SubjectId = subjectId,
                SubjectName = subject.SubjectName
            };
            foreach (var student in studentList)
            {
	            student.IsAdded = subject.OfferedExamStudents.Any(ofs => ofs.StudentId == student.Id && ofs.ExamId == examId);
            }

            assignStudentVm.SubjectStudents = studentList;
            return assignStudentVm;
		}

        /// <summary>
        /// Assigns checked students to the subject of an examination
        /// Removes uncheck students from the subject of an examination
        /// </summary>
        /// <param name="assignViewModel"></param>
        /// <returns></returns>
		public Tuple<int, string> AssignSubjectCandidates(AssignSubjectStudentsViewModel assignViewModel)
		{
			try
			{
				var subject =
					_subjectRepo.FindOneInclude(s => s.Id == assignViewModel.SubjectId, s => s.OfferedExamStudents);
				foreach (var student in assignViewModel.SubjectStudents)
				{
					if (student.IsAdded &&
					    !subject.OfferedExamStudents.Any(es => es.StudentId == student.Id
					                                           && es.ExamId == assignViewModel.ExamId))
					{
                        AssignOrUnAssignStudentSubject(subject, student.Id, assignViewModel.ExamId, Action.Add);
                    }
					else if (!student.IsAdded &&
					         subject.OfferedExamStudents.Any(es => es.StudentId == student.Id
					                                               && es.ExamId == assignViewModel.ExamId))
					{
                        AssignOrUnAssignStudentSubject(subject, student.Id, assignViewModel.ExamId, Action.Remove);
					}
				}

				var result = _subjectRepo.Save();
				if (result < 1)
				{
					return new Tuple<int, string>(result, "Failed to assign students");
				}
				return new Tuple<int, string>(result, "Changes saved");
			}
			catch
			{
				return new Tuple<int, string>(0, "Failed to assign students");

			}
		}

        private void AssignOrUnAssignStudentSubject(Subject subject, int studentId, int examId, Action action)
        {
            switch (action)
            {
                case Action.Add:
                    subject.OfferedExamStudents.Add(new OfferedStudentExamSubject()
                    {
                        ExamId = examId,
                        StudentId = studentId
                    });
                    break;
                case Action.Remove:
                    var offered = subject.OfferedExamStudents.First(es => es.StudentId == studentId
                                                                   && es.ExamId == examId);
                    subject.OfferedExamStudents.Remove(offered);
                    break;
                default:
                    break;
            }
        }

        private void EnrollOrUnEnrollStudent(Examination exam, int studentId, Action action)
        {
            if(action == Action.Add)
            {
                exam.ExaminationStudents.Add(new ExaminationStudent()
                {
                    StudentId = studentId,
                });
            }
            else if(action == Action.Remove)
            {
                var subjects = _subjectRepo.GetSubjectsIncludeEnrolled();
                var examSubjects = subjects.Where(s=>s.OfferedExamStudents.Any(ofs=>ofs.ExamId == exam.Id && ofs.StudentId == studentId));
                foreach(var subject in examSubjects)
                {
                    if(subject.OfferedExamStudents != null)
                    {
                        if(subject.OfferedExamStudents.Any(es => es.StudentId == studentId
                                                                   && es.ExamId == exam.Id))
                        {
                            AssignOrUnAssignStudentSubject(subject, studentId, exam.Id, Action.Remove);
                        }
                    }
                }
                var examStudent = exam.ExaminationStudents.First(es => es.StudentId == studentId);
                exam.ExaminationStudents.Remove(examStudent);
                
            }
        }
    }
}
