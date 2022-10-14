using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Domain.Entities;
using EIMS.Models;

namespace EIMS.Profiles
{
	public class StudentProfile : Profile
	{
		public StudentProfile()
		{
            CreateMap<CreateStudentViewModel, Student>().ReverseMap();
            CreateMap<StudentViewModel, Student>().ReverseMap();
            CreateMap<StudentViewModel, AssignStudentViewModel>().ReverseMap();
            CreateMap<Student, AssignStudentViewModel>().ReverseMap();
            CreateMap<StudentViewModel, StudentTransferViewModel>();
            CreateMap<Student, ExamStudentViewModel>()
                .ForMember(model=>model.StudentName, opt=>opt.MapFrom(st=>st.Name))
                .ReverseMap();
            CreateMap<Student, SubjectStudentViewModel>()
	            .ForMember(model => model.StudentName, opt => opt.MapFrom(st => st.Name))
	            .ReverseMap();

			CreateMap<StudentTransferViewModel, Student>()
	            .ForMember(model => model.LocalGovernmentId, opt => opt.MapFrom(model => model.LocalGovernmentIdTo))
	            .ForMember(model => model.ClassType, opt => opt.MapFrom(model => model.ClassTypeTo))
	            .ForMember(model => model.ClassesId, opt => opt.MapFrom(model => model.ClassesIdTo))
	            .ForMember(model => model.Id, opt => opt.Ignore())
	            .ForMember(model => model.Classes, opt => opt.Ignore())
	            .ForMember(model => model.LocalGovernment, opt => opt.Ignore())
	            .ForMember(model => model.School, opt => opt.Ignore())

	            .ForMember(model => model.SchoolId, opt => opt.MapFrom(model => model.SchoolIdTo));
			
            CreateMap<Student, StudentLog >()
	            .ForMember(model => model.Id, opt => opt.Ignore())
	            .ForMember(model => model.StudentId, opt => opt.MapFrom(student => student.Id ));

        }
	}
}
