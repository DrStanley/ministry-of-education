using AutoMapper;
using EIMS.Domain.Entities;
using EIMS.Models;

namespace EIMS.Profiles
{
    public class ExaminationProfile : Profile
    {
        public ExaminationProfile()
        {
            CreateMap<AddExamViewModel, Examination>().ReverseMap();
            CreateMap<Examination, ExamViewModel>().ReverseMap();
            CreateMap<Examination, AddExamSubjectsViewModel>().ReverseMap();
            CreateMap<Examination, AddExamStudentsViewModel>()
                .ForMember(opt => opt.ExamId, opt => opt.MapFrom(ex => ex.Id));
        }
    }
}
