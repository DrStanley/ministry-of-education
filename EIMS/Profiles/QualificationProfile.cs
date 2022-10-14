using AutoMapper;
using EIMS.Domain.Entities;
using EIMS.Models;

namespace EIMS.Profiles
{
    public class QualificationProfile : Profile 
    {
        public QualificationProfile()
        {
            CreateMap<QualificationViewModel, Qualification>().ReverseMap();
        }
    }
}
