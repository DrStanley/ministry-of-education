using EIMS.Infrastructure.Interfaces;
using EIMS.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EIMS.Infrastructure.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
			services.AddScoped<ISchoolService, SchoolService>();
			services.AddScoped<IProfileService, ProfileService>();
			services.AddScoped<ITeacherService, TeacherService>();
			services.AddScoped<IStudentServices, StudentServices>();
			services.AddScoped<IClassesServices, ClassesServices>();
			services.AddScoped<IExamService, ExamService>();
			services.AddScoped<IWorkExperienceService, WorkExperienceService>();
			services.AddScoped<ISubjectServices, SubjectServices>();
			services.AddScoped<IInspectorServices, InspectorServices>();
			services.AddScoped<IExamEnrollmentService, ExamEnrollmentService>();

			return services;
		}
    }
}
