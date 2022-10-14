using AutoMapper;
using EIMS.Domain.Entities;
using EIMS.Models;

namespace EIMS.Profiles
{
    public class WorkExperienceProfile : Profile
    {
        public WorkExperienceProfile()
        {
            CreateMap<WorkExperienceViewModel, WorkExperience>();
            CreateMap<WorkExperience, WorkExperienceViewModel>();
        }
    }
}
