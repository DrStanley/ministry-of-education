using System.Collections.Generic;
using System.Threading.Tasks;
using EIMS.Domain.Entities;
using EIMS.Models;
using Microsoft.AspNetCore.Http;

namespace EIMS.Infrastructure.Interfaces
{
    public interface ITeacherService
    {
	    Task<string> CreateTeacher(CreateTeacherViewModel teacherViewModel);
	    Task<string> UpdateTeacher(CreateTeacherViewModel teacherViewModel);
	    Task<string> UploadTeachers(IFormFile file);
	    IEnumerable<TeacherViewModel> GetTeachers(string query = null);
	    Task<TeacherViewModel> ChangeStatusAsync(int id);
	    void DeleteTeacherAsync(int id);
	    IEnumerable<TeacherViewModel> GetAssignTeachers(string query = null);
	    AssignTeacherViewModel GetTeacher(int id);
	    IEnumerable<School> GetSchoolByLGA(int id);
	    string AssignTeacher(AssignTeacherViewModel assignTeacherViewModel);
	    string UnAssignTeacher(int id);
	    TransferTeacherViewModel GetTransferTeacherViewModel(int id);
	    string TransferTeacher(TransferTeacherViewModel transferTeacherViewModel);




    }
}
