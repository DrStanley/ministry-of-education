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
	public class SubjectServices :ISubjectServices
	{
		private readonly IMapper _mapper;
		private readonly ISubjectRepo _subjectRepo;

		public SubjectServices(IMapper mapper,
			ISubjectRepo subjectRepo)
		{
			_mapper = mapper;
			_subjectRepo = subjectRepo;

		}

		public string CreateSubject(string subjectName, SchoolCategory subjectLevel)
		{
			var subject = new Subject()
			{
				SubjectName = subjectName,
				 LevelCategory = subjectLevel,
				CreatedAt = DateTime.Now
			};
			try
			{
				if (_subjectRepo.GetAll().Any(s => s.SubjectName == subjectName))
				{
					return "'"+subjectName+"' subject name already exist";
				}
				_subjectRepo.Insert(subject);
				return _subjectRepo.Save() > 0 ? "Success" : "Failed to Save";
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}
		
		public IEnumerable<SubjectViewModel> GetAllSubjects()
		{
			var subjects = _subjectRepo.AllInclude(s => s.SubjectsClasses, s => s.TeachersSubjects);
			return _mapper.Map<IEnumerable<SubjectViewModel>>(subjects);
		}

		public string UpdateStudent(SubjectViewModel viewModel)
		{
			try
			{
				var subject = _subjectRepo.GetById(viewModel.Id);
				subject.SubjectName = viewModel.SubjectName;
				subject.LevelCategory = viewModel.LevelCategory;
				_subjectRepo.Update(subject);
				return _subjectRepo.Save() > 0 ? "Success" : "Failed to Save";
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}
	
		public string RemoveSubject(int id)
		{
			try
			{
				_subjectRepo.Delete(id);
				return _subjectRepo.Save() > 0 ? "Success" : "Failed to Save";
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}

	}
}
