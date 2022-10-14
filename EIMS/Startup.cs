using System;
using EIMS.Domain.Data;
using EIMS.Domain.Identity;
using EIMS.Domain.Interfaces;
using EIMS.Domain.Repository;
using EIMS.Infrastructure.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EIMS
{
    public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();

			//Repositories
			services.AddScoped<ISchoolRepo, SchoolRepo>();
			services.AddScoped<IRoleServices, RoleServices>();
			services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<ITeacherRepo, TeacherRepo>();
            services.AddScoped<ILocalGovernmentRepo, LocalGovernmentRepo>();
            services.AddScoped<IWorkExperienceRepo, WorkExperienceRepo>();
            services.AddScoped<IQualificationRepo, QualificationRepo>();
			services.AddScoped<IProfileRepo, ProfileRepo>();
			services.AddScoped<ITeacherLoggerRepo, TeacherLoggerRepo>();
            services.AddScoped<IExamRepo, ExamRepo>();
			services.AddScoped<IStudentRepo, StudentRepo>();
			services.AddScoped<IClassesRepo, ClassesRepo>();
			services.AddScoped<IStudentLoggerRepo, StudentLoggerRepo>();
			services.AddScoped<ISubjectRepo, SubjectRepo>();
			services.AddScoped<IInspectorRepo, InspectorRepo>();
            services.AddScoped<IExamEnrollmentRepo, ExamEnrollmentRepo>();
            services.AddScoped<IInspectorLoggerRepo, InspectorLoggerRepo>();

			//Services
			services.AddAppServices();

			services.AddSession();
			services.AddHttpContextAccessor();

			services.AddDbContext<ApplicationDbContext>(
		options => options.UseSqlServer("name=ConnectionStrings:EMISDatabase", 
            b => b.MigrationsAssembly("EIMS")));
            services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = false)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			services.AddMvc();
			services.AddRazorPages().AddRazorRuntimeCompilation();
		//	services.AddMemoryCache();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
			UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseSession();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
				   name: "default",
				   pattern: "{controller=Account}/{action=Login}/{id?}");

			});

		}
	}
}
