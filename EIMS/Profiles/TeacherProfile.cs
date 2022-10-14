using AutoMapper;
using EIMS.Domain.Entities;
using EIMS.Domain.Identity;
using EIMS.Models;

namespace EIMS.Profiles
{
	public class TeacherProfile : Profile
	{
		public TeacherProfile()
		{
			CreateMap<CreateTeacherViewModel, AppUser>();
			CreateMap<Teacher, TeacherViewModel>().ReverseMap();

			CreateMap<Teacher, AssignTeacherViewModel>().ForMember(a => a.Name, opt => opt.MapFrom(l => l.User.Name))
				.ForMember(a => a.Email, opt => opt.MapFrom(l => l.User.Email))
				.ForMember(a => a.Id, opt => opt.MapFrom(l => l.Id));
			
			CreateMap<Teacher, TeacherLog>()
				.ForMember(log => log.TeacherId,opt=>opt.MapFrom(teacher => teacher.Id))
				.ForMember(log => log.Id,opt=>opt.Ignore());

			CreateMap<Teacher, TransferTeacherViewModel>()
				.ForMember(trans => trans.Email, opt => opt.MapFrom(teacher => teacher.User.Email))
				.ForMember(trans => trans.Name, opt => opt.MapFrom(teacher => teacher.User.Name));
			
			CreateMap<TransferTeacherViewModel, Teacher >()
				.ForMember(model => model.Id,opt=>opt.Ignore())
				.ForMember(model => model.LocalGovernmentId,opt=>opt.MapFrom(model => model.LocalGovernmentIdTo))
				.ForMember(model => model.SchoolId,opt=>opt.MapFrom(model => model.SchoolIdTo));
				CreateMap<TransferTeacherViewModel, TeacherLog >()
				.ForMember(model => model.Id,opt=>opt.Ignore())
				.ForMember(model => model.TeacherId,opt=>opt.MapFrom(model => model.Id))
				.ForMember(model => model.LocalGovernmentId,opt=>opt.MapFrom(model => model.LocalGovernmentIdTo))
				.ForMember(model => model.SchoolId,opt=>opt.MapFrom(model => model.SchoolIdTo));
				CreateMap<TeacherViewModel,CreateTeacherViewModel>()
					.ForMember(t=>t.PhoneNumber,opt => opt.MapFrom(t=>t.User.PhoneNumber))
					.ForMember(t=>t.Title,opt => opt.MapFrom(t=>t.User.Title));

		}

	}
}