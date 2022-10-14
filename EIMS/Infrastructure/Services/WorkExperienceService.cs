using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Domain.Entities;
using EIMS.Domain.Interfaces;
using EIMS.Infrastructure.Interfaces;
using EIMS.Models;

namespace EIMS.Infrastructure.Services
{
    public class WorkExperienceService : IWorkExperienceService
    {
        private readonly IMapper _mapper;
        private readonly IWorkExperienceRepo _workExpRepo;

        public WorkExperienceService(
                IWorkExperienceRepo workExpRepo,
                IMapper mapper
            )
        {
            _mapper = mapper;
            _workExpRepo = workExpRepo;

        }

        public Tuple<int, string> AddOrUpdateWorkExperience(WorkExperienceViewModel wXpViewModel, int worExpId)
        {
            var workExperience = _mapper.Map<WorkExperience>(wXpViewModel);
            var successMessage = "";
            if (worExpId == 0)
            {
                _workExpRepo.Insert(workExperience);
                successMessage = "Added work experience";
            }
            else
            {
                _workExpRepo.Update(workExperience);
                successMessage = "Changes have been saved";
            }

            int result = _workExpRepo.Save();
            if (result > 0)
            {
                return new Tuple<int, string>(result, successMessage);
            }

            var action = worExpId == 0 ? "Add" : "Update";
            return new Tuple<int, string>(result, $"Failed to {action} work experience");
        }

        public Tuple<int, string> DeleteWorkExperience(int workExpId)
        {
            var experience = _workExpRepo.GetById(workExpId);
            if (experience == null)
            {
                throw new Exception("Could not find work experine");
            }

            _workExpRepo.Delete(workExpId);
            var result = _workExpRepo.Save();
            return new Tuple<int, string>(result, "Experience has been deleted");
        }

        public IList<WorkExperience> GetTeacherWorkExperiences(int teacherId)
        {
            return _workExpRepo.Find(w=>w.TeacherId==teacherId).OrderBy(w=>w.StartDate).ToList();
        }

        public WorkExperienceViewModel GetWorkExperience(int workExpId)
        {
            return _mapper.Map<WorkExperienceViewModel>(_workExpRepo.GetById(workExpId));
        }
    }
}
