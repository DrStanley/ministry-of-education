using System;
using System.Collections.Generic;
using System.Text;
using EIMS.Domain.Data;
using EIMS.Domain.Entities;
using EIMS.Domain.Interfaces;

namespace EIMS.Domain.Repository
{
    public class WorkExperienceRepo : GenericRepo<WorkExperience>, IWorkExperienceRepo
    {
        public WorkExperienceRepo(ApplicationDbContext context)
        : base(context)
        {
            
        }
    }
}
