
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;
using EIMS.Models;
using Microsoft.AspNetCore.Http;

namespace EIMS.Infrastructure.Interfaces
{
    public interface ISchoolService
    {
        Task<string> AddNewSchool(CreateSchoolViewModel schoolViewModel);
        School GetSchool(int schoolId);
        string EditSchoolDetails(EditSchoolViewModel schoolViewModel);
        IEnumerable<SchoolViewModel> GetSchools(string query = null);
		Task<string> UploadSchoolAsync(IFormFile formFile);
		SchoolViewModel ChangeStatus(int id);
		void DeleteSchoolAsync(int schoolId);

    }
}
