using AutoMapper;
using EIMS.Domain.Entities;
using EIMS.Models;

namespace EIMS.Profiles
{
    public class UserDetailsProfile : Profile   
    {
        public UserDetailsProfile()
        {
            CreateMap<ProfileViewModel, UserProfile>().ReverseMap();
        }
    }
}
