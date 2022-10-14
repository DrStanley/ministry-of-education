using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Domain.Entities;
using EIMS.Domain.Identity;
using EIMS.Models;

namespace EIMS.Profiles
{
	public class InspectorProfile : Profile
	{
		public InspectorProfile()
		{
			CreateMap<CreateInspectorViewModel, AppUser>().ReverseMap();
			CreateMap<CreateInspectorViewModel, Inspector>().ReverseMap();
			CreateMap<CreateInspectorViewModel, InspectorViewModel>().ReverseMap();
			CreateMap<InspectorViewModel, TransferInspectorViewModel>().ReverseMap();
			CreateMap<TransferInspectorViewModel, InspectorLog>()
				.ForMember(i=>i.InspectorId,opt=>opt.MapFrom(i=>i.Id))
				.ForMember(i=>i.Id,opt=>opt.Ignore()).ReverseMap();
			CreateMap<Inspector,InspectorLog>()
				.ForMember(i=>i.Id,opt=>opt.Ignore())
				.ForMember(i=>i.InspectorId,opt=>opt.MapFrom(i=>i.Id));
			CreateMap<Inspector, InspectorViewModel>()
				.ForMember(i =>i.Name,opt=>opt.MapFrom(i=>i.User.Name))
				.ForMember(i =>i.Email,opt=>opt.MapFrom(i=>i.User.Email))
				.ForMember(i =>i.Title,opt=>opt.MapFrom(i=>i.User.Title))
				.ForMember(i =>i.PhoneNumber,opt=>opt.MapFrom(i=>i.User.PhoneNumber))
				.ReverseMap();
		}
	}
}
