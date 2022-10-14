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
	public class ClassesServices : IClassesServices
	{
		private readonly IMapper _mapper;
		private readonly IClassesRepo _classesRepo;

		public ClassesServices(IMapper mapper,
			IClassesRepo classesRepo)
		{
			_mapper = mapper;
			_classesRepo = classesRepo;

		}

		public string CreateClass(string className, ClassType classType)
		{

			var clas = new Classes()
			{
				ClassType = classType,
				ClassName = className,
				CreatedAt = DateTime.Now
			};
			try
			{
				_classesRepo.Insert(clas);
				return _classesRepo.Save() > 0 ? "Success" : "Failed to Save";
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}
		
		public string RemoveClass(int id)
		{
			try
			{
				_classesRepo.Delete(id);
				return _classesRepo.Save() > 0 ? "Success" : "Failed to Delete";
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}
		public string UpdateClass(ClassViewModel viewModel)
		{
			try
			{
				var clas = _classesRepo.GetById(viewModel.Id);
				clas.ClassName = viewModel.ClassName;
				clas.ClassType = viewModel.ClassType;
				_classesRepo.Update(clas);
				return _classesRepo.Save() > 0 ? "Success" : "Failed to Save";
			}
			catch (Exception e)
			{
				return e.Message;
			}
		}

		public IEnumerable<ClassViewModel> GetAllClasses(string query=null)
		{
			var classes = _mapper.Map<IEnumerable<ClassViewModel>>(_classesRepo.AllInclude(l => l.SubjectsClasses)).OrderBy(c=>c.ClassType);

			return query switch
			{
				"primary" => classes.Where(s => s.ClassType == ClassType.PRIMARY),
				"secondary" => classes.Where(s => s.ClassType==ClassType.SECONDARY),
				_ => classes
			};
		}
	}
}
