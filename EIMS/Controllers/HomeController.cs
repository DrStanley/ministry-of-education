using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Domain.Interfaces;
using EIMS.Infrastructure.Interfaces;
using EIMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.Controllers
{
	[Authorize]
	public class HomeController : BaseController
	{
		private readonly ISchoolService _schoolServices;
		private readonly ITeacherService _teacherService;
		private readonly ILocalGovernmentRepo _localGovernmentRepo;
		private readonly IStudentServices _studentServices;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ITeacherLoggerRepo _loggerRepo;
		private readonly ISchoolRepo _schoolRepo;
		private readonly IInspectorServices _inspectorServices;
		private readonly IInspectorLoggerRepo _inspectorLoggerRepo;
		public HomeController(ISchoolService schoolServices, ITeacherService teacherService, 
			ILocalGovernmentRepo localGovernmentRepo, IHttpContextAccessor httpContextAccessor,
			ITeacherLoggerRepo loggerRepo, IStudentServices studentServices, IInspectorServices inspectorServices, IInspectorLoggerRepo inspectorLoggerRepo, ISchoolRepo schoolRepo)
		{
			_schoolServices = schoolServices;
			_teacherService = teacherService;
			_localGovernmentRepo = localGovernmentRepo;
			_httpContextAccessor = httpContextAccessor;
			_loggerRepo = loggerRepo;
			_studentServices= studentServices;
			_inspectorServices = inspectorServices;
			_inspectorLoggerRepo = inspectorLoggerRepo;
			_schoolRepo = schoolRepo;
		}
		[Route("Admin")]
		public IActionResult Index()
		{
			if (User.IsInRole("SchoolAdmin"))
			{
				return RedirectToAction("SchoolIndex");
			}
			if (User.IsInRole("Teacher"))
			{
				return RedirectToAction("TeacherIndex");
			}	
			if (User.IsInRole("Inspector"))
			{
				return RedirectToAction("InspectorIndex");
			}

			var model = new AdminViewModel() {
				LocalGovernments = _localGovernmentRepo.GetAll(),
				Schools = _schoolServices.GetSchools(),
				Teachers = _teacherService.GetTeachers(),
				Students = _studentServices.GetAllStudents("")
			};
			return View(model);
		}

		[Route("School")]
		public IActionResult SchoolIndex()
		{
			var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
			var model = _schoolServices.GetSchools().FirstOrDefault(s => s.Email == email);
			return View(model);
		}

		[Route("Teacher")]
		public IActionResult TeacherIndex()
		{
			var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			var model = _teacherService.GetAssignTeachers().FirstOrDefault(t => t.UserId==userId);
			var logs = _loggerRepo.GetAll().Where(log => log.TeacherId == model.Id).OrderByDescending(l=>l.CreatedAt);
			model.TeacherLogs = logs;
			return View(model);
		}
		
		[Route("Inspector")]
		public IActionResult InspectorIndex()
		{
			var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
			var model = _inspectorServices.GetInspectors().FirstOrDefault(t => t.UserId==userId);
			var logs = _inspectorLoggerRepo.GetAll().Where(log => log.InspectorId == model.Id).OrderByDescending(l=>l.CreatedAt);
			var count = _schoolRepo.GetAllSchoolByLga(model.LocalGovernmentId.Value).Count();
			model.InspectorLogs = logs;
			model.SchoolsCount = count;
			return View(model);
		}
	}
}
