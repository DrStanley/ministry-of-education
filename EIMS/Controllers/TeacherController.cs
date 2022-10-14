using System;
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
	[Authorize(Roles = "Admin")]
	public class TeacherController : BaseController
	{
		private readonly ITeacherService _teacherService;
		private readonly IMapper _mapper;

		public TeacherController(ITeacherService teacherService, IMapper mapper)
		{
			_teacherService = teacherService;
			_mapper = mapper;
		}
		
		public IActionResult AddTeacher()
		{
			return View();
		}

		

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddTeacher(CreateTeacherViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var result = await _teacherService.CreateTeacher(viewModel);
				if (result == "Success")
				{
					SweetAlert("Success", $"{viewModel.Name} has been added", NotificationType.success);
                    return RedirectToAction("AllTeachers");
				}

				ModelState.AddModelError("", result);
				return View(viewModel);
			}
			ModelState.AddModelError("", "Invalid data");
			return View(viewModel);
		}
		public IActionResult EditTeacher(int id)
		{
			return View(_mapper.Map <CreateTeacherViewModel> (_teacherService.GetTeachers().FirstOrDefault(t=> t.Id==id)));
		}

		

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditTeacher(CreateTeacherViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var result = await _teacherService.UpdateTeacher(viewModel);
				if (result == "Success")
				{
					SweetAlert("Success", $"{viewModel.Name} has been Updated", NotificationType.success);
					return RedirectToAction("AllTeachers", "Teacher");
				}

				ModelState.AddModelError("", result);
				return View(viewModel);
			}
			ModelState.AddModelError("", "Invalid data");
			return View(viewModel);
		}


		
		public IActionResult UploadTeachers()	
		{
			return View();
		}

		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UploadTeachers(IFormFile file)
		{
			if (file != null)
			{
				var res = await _teacherService.UploadTeachers(file);
				if (res == "Success")
				{
					SweetAlert(file.FileName, "Upload Successful", NotificationType.success);
					return RedirectToAction("AllTeachers", "Teacher");
				}
				TempData["UploadError"] = res;
				return View(file);
			}
			TempData["UploadError"] = "Select a file";
			return View();
		}
		
		public IActionResult AllTeachers()
		{

			var teachers = _teacherService.GetTeachers();
			return View(teachers);
		}

		
		public async Task<IActionResult> ChangeTeacherStatus(int id)
		{
			var change = await _teacherService.ChangeStatusAsync(id);
			var stat = Enum.GetName(typeof(Status), change.Status);

			SweetAlert(stat.ToUpper(), change.Name+" status is " + stat, NotificationType.info);
			return RedirectToAction("AllTeachers");
		}

		
		public IActionResult DeleteTeacher(int id)
		{
			_teacherService.DeleteTeacherAsync(id);
			SweetAlert("Deleted", "Teacher deleted successful", NotificationType.error);

			return RedirectToAction("AllTeachers");
		}

		
		public IActionResult AssignView(string type = "assign")
		{
			TempData["typeA"] = type == "assign" ? "unassign" : "assign";

			var teachers = _teacherService.GetAssignTeachers(type);
			return View(teachers);
		}


		
		public IActionResult AssignTeacher(int id)
		{
			var model = _teacherService.GetTeacher(id);
			return View(model);
		}

		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AssignTeacher(AssignTeacherViewModel assignTeacherViewModel)
		{
			if (assignTeacherViewModel.SchoolId != 0 && 
                assignTeacherViewModel.LocalGovernmentId != 0)
			{
				var res = _teacherService.AssignTeacher(assignTeacherViewModel);
				if (res == "Success")
				{
					SweetAlert("Assigned", assignTeacherViewModel.Name + " has been assigned a school", NotificationType.success);
					return RedirectToAction("AllTeachers");
				}
				TempData["AssignError"] = res;
				return RedirectToAction("AssignTeacher", new { id = assignTeacherViewModel.Id });
			}
			TempData["AssignError"] = "Please Choose Local government and School";
			return RedirectToAction("AssignTeacher", new { id = assignTeacherViewModel.Id });
		}
		
		
		public IActionResult UnassignTeacher(int id)
		{
			
				var res = _teacherService.UnAssignTeacher(id);
				if (res == "Success")
				{
					SweetAlert("Unassigned",  "Teacher has been unassigned", NotificationType.info);
					return RedirectToAction("AllTeachers");
				}
				SweetAlert("Error", res, NotificationType.error);
				return RedirectToAction("AllTeachers");

		}

		public JsonResult GetSchoolsByLga(string id)
		{
			return Json(_teacherService.GetSchoolByLGA(Convert.ToInt32(id))
                .OrderBy(school => school.SchoolName).ToList());
		}

		public IActionResult TransferTeacher( int id)
		{
			var transferTeacherViewModelModel = _teacherService.GetTransferTeacherViewModel(id);

			return View(transferTeacherViewModelModel);
		}
		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult TransferTeacher(TransferTeacherViewModel   transferTeacherViewModel)
		{
			if (transferTeacherViewModel.SchoolIdTo != 0 && transferTeacherViewModel.LocalGovernmentIdTo != 0)
			{
				var res = _teacherService.TransferTeacher(transferTeacherViewModel);
				if (res == "Success")
				{
					SweetAlert("Transfer", transferTeacherViewModel.Name + " has been transferred",
						NotificationType.success);
					return RedirectToAction("AssignView");
				}

				SweetAlert("Error", res, NotificationType.error);
				return RedirectToAction("TransferTeacher", new {id = transferTeacherViewModel.Id});
			}
			SweetAlert("Error", "Please Choose Transfer Local government and School", NotificationType.error);

			return RedirectToAction("TransferTeacher", new { id = transferTeacherViewModel.Id });
		}
	}
}
