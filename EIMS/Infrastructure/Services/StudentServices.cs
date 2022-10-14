using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;
using EIMS.Domain.Identity;
using EIMS.Domain.Interfaces;
using EIMS.Infrastructure.Interfaces;
using EIMS.Models;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;

namespace EIMS.Infrastructure.Services
{
	public class StudentServices : IStudentServices
	{
		private readonly IMapper _mapper;
		private readonly IStudentRepo _studentRepo;
		private readonly ISchoolRepo _schoolRepo;
		private readonly IStudentLoggerRepo _loggerRepo;

		public StudentServices(IMapper mapper,
			IStudentRepo studentRepo,
			ISchoolRepo schoolRepo,
			IStudentLoggerRepo loggerRepo)
		{
			_mapper = mapper;
			_studentRepo = studentRepo;
			_schoolRepo = schoolRepo;
			_loggerRepo = loggerRepo;
		}
		public string CreateStudent(CreateStudentViewModel ViewModel)
		{
			var student = _mapper.Map<Student>(ViewModel);
			try
			{
				student.CreatedAt = DateTime.Now;
				_studentRepo.Insert(student);
				return _studentRepo.Save() > 0 ? "Success" : "Failed to Save";
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}
		public string UpdateStudent(CreateStudentViewModel studentViewModel)
		{
			try
			{
				var currentStudent = _studentRepo.GetById(studentViewModel.Id);
				_mapper.Map(studentViewModel, currentStudent);
				_studentRepo.Update(currentStudent);
				return _studentRepo.Save() > 0 ? "Success" : "Failed to Save";
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}
		public string RemoveStudent(int id)
		{

			try
			{
				_studentRepo.Delete(id);
				return _studentRepo.Save() > 0 ? "Success" : "Failed to Save";
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}

		public CreateStudentViewModel GetStudent(int id)
		{
			return _mapper.Map<CreateStudentViewModel>(_studentRepo.GetById(id));
		}


		public async Task<string> UploadStudents(IFormFile file)
		{
			try
			{
				var error = "";
				var (createStudentViewModels, item2) = await ExtractStudentsTasks(file);
				if (item2 == "Success")
				{
					if (createStudentViewModels.Count > 0)
					{
						var count = 0;
						foreach (var student in createStudentViewModels)
						{
							count++;
							var res = CreateStudent(student);
							if (res != "Success")
							{
								error += $"\n{count}. " + res + " student name: " + student.Name + "\n";
							}
						}

						return error != "" ? error : "Success";

					}

					return "No data found";
				}

				return item2;
			}
			catch (Exception e)
			{
				return e.Message;
			}
		
		}

		public static async Task<(List<CreateStudentViewModel>,string)> ExtractStudentsTasks(IFormFile file)
		{
			var studentsList = new List<CreateStudentViewModel>();
			try
			{
				var stream = new MemoryStream();
				await file.CopyToAsync(stream);

				var package = new ExcelPackage(stream);
				var worksheet = package.Workbook.Worksheets[0];
				var rowCount = worksheet.Dimension.Rows;
				for (var row = 2; row <= rowCount; row++)
				{
					studentsList.Add(new CreateStudentViewModel()
					{
						Name = worksheet.Cells[row, 1].Value.ToString(),
						Gender = Enum.Parse<Gender>(worksheet.Cells[row, 2].Value.ToString().ToUpper()),
						StateOfOrigin = (States)Enum.Parse(typeof(States), worksheet.Cells[row, 3].Value.ToString().Replace(" ", "_")),
						DateOfBirth = DateTime.Parse(worksheet.Cells[row, 4].Value.ToString()),
						NextOfKinName = worksheet.Cells[row, 5].Value.ToString(),
						NextOfKinRelation = worksheet.Cells[row, 6].Value.ToString(),
						NextOfContact = worksheet.Cells[row, 7].Value.ToString(),
					});
				}
				return (studentsList, "Success");

			}
			catch (Exception e)
			{
				return (studentsList, e.Message);
			}


		}

		public IEnumerable<StudentViewModel> GetAllStudents(string query=null)
		{
			var students = _studentRepo.AllInclude(l => l.LocalGovernment, s => s.School, c => c.Classes);
			var studentViewModels = _mapper.Map<IEnumerable<StudentViewModel>>(students).OrderBy(s=>s.Name);
			return query switch
			{
				"Assigned" => studentViewModels.Where(c => c.ClassesId != null && c.LocalGovernmentId != null && c.SchoolId != null).OrderBy(s => s.School.SchoolName).OrderBy(s => s.School.SchoolName),
				"Unassigned" => studentViewModels.Where(c => c.ClassesId == null || c.LocalGovernmentId == null),
				_ => studentViewModels
			};

		}

		public string AssignStudent(AssignStudentViewModel assignStudentViewModel)
		{
			var transaction = _studentRepo.GetDbContextTransaction();

			try
			{
				var student = _studentRepo.GetById(assignStudentViewModel.Id);
				_mapper.Map(assignStudentViewModel, student);
				_studentRepo.Update(student);
				_studentRepo.Save();
				var savedStudent = _studentRepo.FindOneInclude(std => std.Id == assignStudentViewModel.Id,
					l => l.LocalGovernment, s => s.School, c => c.Classes);

				var log = _mapper.Map<StudentLog>(student);
				log.LogType = LogType.Assign;
				log.Details = student.Name + " was assigned to "+ savedStudent.ClassType.ToString()
				              + " Class: "+ savedStudent.Classes.ClassName + ", LGA: " + savedStudent.LocalGovernment.Name +
				              "and School: " + savedStudent.School.SchoolName;
				log.CreatedAt = DateTime.Now;
				_loggerRepo.Insert(log);
				_loggerRepo.Save();

				transaction.Commit();
				return "Success";
			}
			catch (Exception e)
			{
				transaction.Rollback();
				return e.Message;
			}
		}
		
		public string UnAssignStudent(int id)
		{
			var transaction =  _studentRepo.GetDbContextTransaction();

			try
			{
				var student = _studentRepo.AllInclude(l => l.LocalGovernment, s => s.School, c => c.Classes).FirstOrDefault(s => s.Id == id);

				var log = _mapper.Map<StudentLog>(student);
				log.LogType = LogType.UnAssign;
				log.Details = student.Name + " was unassigned from LGA: " + student.LocalGovernment.Name +
							  "and School: " + student.School.SchoolName;
				log.CreatedAt = DateTime.Now;
				_loggerRepo.Insert(log);

				student.LocalGovernmentId = null;
				student.LocalGovernment = null;
				student.School = null;
				student.SchoolId = null;
				student.Classes = null;
				student.ClassesId = null;
				student.ClassType = null;
				_studentRepo.Update(student);
				_studentRepo.Save();
				transaction.Commit();
				return "Success";
			}
			catch (Exception e)
			{
				transaction.Rollback();
				return e.Message;
			}
		}

		public IEnumerable<School> GetSchoolByLGA(int id)
		{
			var schools = _schoolRepo.GetAllSchoolByLga(id);
			return schools;
		}

		public string TransferStudent(StudentTransferViewModel studentViewModel)
		{

			var transaction = _studentRepo.GetDbContextTransaction();

			try
			{
				var currentStudent = _studentRepo.GetById(studentViewModel.Id);
				_mapper.Map(studentViewModel, currentStudent);
				_studentRepo.Update(currentStudent);
				_studentRepo.Save();

				var savedStudent = _studentRepo.FindOneInclude(std => std.Id == studentViewModel.Id,
					l => l.LocalGovernment, s => s.School, c => c.Classes);


				var log = _mapper.Map<StudentLog>(savedStudent);
				log.LogType = LogType.Transfer;

				log.Details = studentViewModel.Name + " was transferred from Class: "+studentViewModel.Classes.ClassName+" LGA: " + studentViewModel.LocalGovernment.Name +
				              "and School: " + studentViewModel.School.SchoolName + ", to Class: "+ savedStudent.Classes.ClassName + ", LGA: " + savedStudent.LocalGovernment.Name +
					"and School: " + savedStudent.School.SchoolName;
				log.CreatedAt = DateTime.Now;
				_loggerRepo.Insert(log);
				_loggerRepo.Save();
			transaction.Commit();
				return "Success";
			}
			catch (Exception e)
			{
				transaction.Rollback();
				return e.Message;
			}
		}
	}
}
