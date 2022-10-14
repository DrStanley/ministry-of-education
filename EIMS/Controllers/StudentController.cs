using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Domain.Entities.Enum;
using EIMS.Domain.Interfaces;
using EIMS.Infrastructure.Interfaces;
using EIMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.Controllers
{
	[Authorize(Roles = "Admin,SchoolAdmin")]

	public class StudentController : BaseController
	{

		private readonly IStudentServices _studentServices;
		private readonly ISchoolService _schoolService;
		private readonly ILocalGovernmentRepo _localGovernmentRepo;
		private readonly IClassesRepo _classesRepo;
		private readonly IMapper _mapper;

		public StudentController(IStudentServices studentServices, IMapper mapper,
			ILocalGovernmentRepo localGovernmentRepo, IClassesRepo classesRepo,
			ISchoolService schoolService)
		{
			_studentServices = studentServices;
			_mapper = mapper;
			_localGovernmentRepo = localGovernmentRepo;
			_classesRepo = classesRepo;
			_schoolService = schoolService;
		}
		
		public IActionResult AllStudent(string type = "Assigned")
		{
			TempData["type"] = type == "Assigned" ? "Unassigned" : "Assigned";

			var students = _studentServices.GetAllStudents(type);
			return View(students);
		}
		public IActionResult AddStudent()
		{
			ViewBag.LocalGovernments = _localGovernmentRepo.GetAll();
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AddStudent(CreateStudentViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var result =  _studentServices.CreateStudent(viewModel);
				if (result == "Success")
				{
					SweetAlert(viewModel.Name, "Student Added", NotificationType.success);
					return RedirectToAction("AllStudent");
				}
				ModelState.AddModelError("message", result);
				return View(viewModel);
			}
			TempData["Alert"] = ModelState.Values.ToString();
			ModelState.AddModelError("message", ModelState.Values.ToString());
			return View(viewModel);

		}

		public IActionResult EditStudent(int id)
		{
			return View(_studentServices.GetStudent(id));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditStudent(CreateStudentViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var result = _studentServices.UpdateStudent(viewModel);
				if (result == "Success")
				{
					SweetAlert(viewModel.Name, "Student Updated", NotificationType.success);
					return RedirectToAction("AllStudent", "Student");
				}
				ModelState.AddModelError("message", result);
				return View(viewModel);
			}
			TempData["Alert"] = ModelState.Values.ToString();
			ModelState.AddModelError("message", ModelState.Values.ToString());
			return View(viewModel);

		}
		
		public IActionResult UploadStudents()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UploadStudents(IFormFile file)
		{
			if (file != null)
			{
				var res = await _studentServices.UploadStudents(file);
				if (res == "Success")
				{
					SweetAlert(file.FileName, "Upload Successful", NotificationType.success);
					return RedirectToAction("AllStudent", "Student");
				}
				TempData["UploadError"] = res;
				return View(file);
			}
			TempData["UploadError"] = "Select a file";
			return View();
		}
		
		public IActionResult AssignStudent(int id)
		{
			var model = _mapper.Map<AssignStudentViewModel>(_studentServices.GetAllStudents("")
				.FirstOrDefault(s => s.Id == id));
			model.LocalGovernments = _localGovernmentRepo.GetAll();
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AssignStudent(AssignStudentViewModel assignStudentViewModel)
		{
			if (assignStudentViewModel.SchoolId != 0 &&
			    assignStudentViewModel.LocalGovernmentId != 0)
			{
				var res = _studentServices.AssignStudent(assignStudentViewModel);
				if (res == "Success")
				{
					SweetAlert("Assigned", assignStudentViewModel.Name + " has been assigned a school", NotificationType.success);
					return RedirectToAction("AllStudent");
				}
				TempData["AssignError"] = res;
				return RedirectToAction("AssignStudent", new { id = assignStudentViewModel.Id });
			}
			TempData["AssignError"] = "Please Choose Local government and School";
			return RedirectToAction("AssignStudent", new { id = assignStudentViewModel.Id });
		}
		
		public JsonResult GetSchoolsByLga(string id)
		{
			return Json(_studentServices.GetSchoolByLGA(Convert.ToInt32(id))
				.OrderBy(school => school.SchoolName).ToList());
		}	
		
		public JsonResult GetClassByType(string id)
		{
			var classType = (ClassType)Convert.ToInt32(id);
			return Json(_classesRepo.FindByInclude(classes => classes.ClassType==classType).OrderBy(classes => classes.ClassName).ToList());
		}

		public IActionResult DeleteStudent(int id)
		{

			var res =_studentServices.RemoveStudent(id);
			if (res== "Success")
			{
			 SweetAlert("Deleted", "Student Removed Successful", NotificationType.success);
			}
			else
			{
				SweetAlert("Error", res, NotificationType.error);

			}

			return RedirectToAction("AllStudent");
		}

		public IActionResult UnassignStudent(int id)
		{
			var res = _studentServices.UnAssignStudent(id);
			if (res == "Success")
			{
				SweetAlert("Unassigned", "Student has been unassigned", NotificationType.info);
				return RedirectToAction("AllStudent");
			}
			SweetAlert("Error", res, NotificationType.error);
			return RedirectToAction("AllStudent");

		}

		public IActionResult StudentTransfer(int id)
		{
			var model = _mapper.Map<StudentTransferViewModel>(_studentServices.GetAllStudents("")
				.FirstOrDefault(s => s.Id == id));
			model.LocalGovernments = _localGovernmentRepo.GetAll();
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult StudentTransfer(StudentTransferViewModel studentTransferViewModel)
		{

			if (studentTransferViewModel.SchoolIdTo != 0 &&
			    studentTransferViewModel.LocalGovernmentIdTo != 0)
			{
				var res = _studentServices.TransferStudent(studentTransferViewModel);
				if (res == "Success")
				{
					SweetAlert("Transfered", studentTransferViewModel.Name + " has been Transferred", NotificationType.success);
					return RedirectToAction("AllStudent");
				}
				TempData["AssignError"] = res;
				return RedirectToAction("StudentTransfer", new { id = studentTransferViewModel.Id });
			}
			TempData["AssignError"] = "Please Choose Local government and School";
			return RedirectToAction("StudentTransfer", new { id = studentTransferViewModel.Id });
		}

		public IActionResult StudentSchool(string email)
		{
			var school = _schoolService.GetSchools().FirstOrDefault(s => s.Email == email);
			var students = _studentServices.GetAllStudents().Where(s => s.SchoolId == school.Id).OrderBy(s=>s.Name);

			return View(students);

		}
	}
}
