using System;
using System.Collections.Generic;
using EIMS.Domain.Entities;
using EIMS.Models;

namespace EIMS.Infrastructure.Interfaces
{
    public interface IWorkExperienceService
    {
        IList<WorkExperience> GetTeacherWorkExperiences(int teacherId);
        WorkExperienceViewModel GetWorkExperience(int workExpId);
        Tuple<int, string> AddOrUpdateWorkExperience(WorkExperienceViewModel wXpViewModel, int worExpId);
        Tuple<int, string> DeleteWorkExperience(int workExpId);
    }
}
