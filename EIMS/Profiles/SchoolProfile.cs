using AutoMapper;
using EIMS.Domain.Entities;
using EIMS.Domain.Identity;
using EIMS.Models;

namespace EIMS.Profiles
{
    public class SchoolProfile : Profile
    {
        public SchoolProfile()
        {
            CreateMap<School, SchoolViewModel>()
                .ForMember(s=>s.Category, opt => opt.MapFrom(sch =>sch.SchoolCategory))
                .ForMember(s=>s.Type, opt => opt.MapFrom(sch =>sch.SchoolType))
                .ForMember(s=>s.LgaName, opt => opt.MapFrom(sch =>sch.LocalGovernment.Name));

            CreateMap<CreateSchoolViewModel, School>().ReverseMap();
            CreateMap<School, EditSchoolViewModel>().ReverseMap()
                .ForMember(s => s.Status, opt => opt.Ignore())
                .ForMember(s => s.UserId, opt => opt.Ignore())
                .ForMember(s => s.User, opt => opt.Ignore());

            CreateMap<CreateSchoolViewModel, AppUser>()
                .ForMember(a=> a.Name,opt=>opt.MapFrom(l=>l.PrincipalName))
                .ForMember(a=> a.UserName,opt=>opt.MapFrom(l=>l.Email));
              CreateMap<School, AppUser>()
                .ForMember(a=> a.Name,opt=>opt.MapFrom(l=>l.PrincipalName))
                .ForMember(a=> a.UserName,opt=>opt.MapFrom(l=>l.Email));

        }
    }
}
