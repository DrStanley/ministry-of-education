using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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
	[Authorize]
	public class SchoolController : BaseController
	{
		private readonly ISchoolService _schoolServices;
        private readonly IMapper _mapper;
        private readonly ILocalGovernmentRepo _localGovernmentRepo;

        public SchoolController(ISchoolService schoolServices, IMapper mapper, ILocalGovernmentRepo localGovernmentRepo)
		{
			_schoolServices = schoolServices;
            _mapper = mapper;
            _localGovernmentRepo = localGovernmentRepo;
        }
		public IActionResult Index()
		{
			return View();
		}

		//Get
		public IActionResult AddSchool()
        {
	        var schVm = new CreateSchoolViewModel() {LocalGovernments = _localGovernmentRepo.GetAll()};
	        return View(schVm);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> AddSchool(CreateSchoolViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var result = await _schoolServices.AddNewSchool(viewModel);

				if (result =="Success")
				{
					SweetAlert(viewModel.SchoolName, "School Added", NotificationType.success);
					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("message", result);
				return View(viewModel);
			}
			TempData["Alert"] = ModelState.Values.ToString();
			ModelState.AddModelError("message", "Invalid data");
			return View(viewModel);
		}

		public IActionResult AllSchools(string type=null) {
			var schools = _schoolServices.GetSchools(type);
			return View(schools);
		}

		public JsonResult GetSchools()
		{
			try
			{

				var draw = Request.Form["draw"].FirstOrDefault();
				var start = Request.Form["start"].FirstOrDefault();
				var length = Request.Form["length"].FirstOrDefault();
				/*var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();*/
				string searchValue = Request.Form["search[value]"].FirstOrDefault();
				int pageSize = length != null ? Convert.ToInt32(length) : 0;
				int skip = start != null ? Convert.ToInt32(start) : 0;
				// int recordsTotal = 0;

				var schools = _schoolServices.GetSchools("active");

				/*if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                }*/

				if (!string.IsNullOrEmpty(searchValue))
				{
					schools = schools.Where(s => s.SchoolName.Contains(searchValue));
				}
				var schoolCount = schools.Count();

				var data = schools.Skip(skip).Take(pageSize).ToList();

				// Paging Length 10,20  
				return Json(new { draw, recordsFiltered = schoolCount, recordsTotal = schoolCount, data });
			}
			catch
			{
				throw;
			}
		}
		public JsonResult GetInSchools()
		{
			try
			{

				var draw = Request.Form["draw"].FirstOrDefault();
				var start = Request.Form["start"].FirstOrDefault();
				var length = Request.Form["length"].FirstOrDefault();
                /*var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();*/
                string searchValue = Request.Form["search[value]"].FirstOrDefault();
				int pageSize = length != null ? Convert.ToInt32(length) : 0;
				int skip = start != null ? Convert.ToInt32(start) : 0;
				// int recordsTotal = 0;

				var schools = _schoolServices.GetSchools("inactive");

                /*if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
					schools.Where()
                }*/

                if (!string.IsNullOrEmpty(searchValue))
				{
					schools = schools.Where(s => s.SchoolName.Contains(searchValue));
				}
				var schoolCount = schools.Count();

				var data = schools.Skip(skip).Take(pageSize).ToList();

				// Paging Length 10,20  
				return Json(new { draw, recordsFiltered = schoolCount, recordsTotal = schoolCount, data });
			}
			catch
			{
				throw;
			}
		}

		[HttpGet]
		public IActionResult EditSchool(int id)
		{
			var school = _mapper.Map<EditSchoolViewModel>(_schoolServices.GetSchool(id));
            school.LocalGovernments = _localGovernmentRepo.GetAll();
			ViewBag.SchoolName = school.SchoolName;
			return View(school);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult EditSchool(EditSchoolViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var result = _schoolServices.EditSchoolDetails(viewModel);
				if (result== "Success")
				{
					SweetAlert(viewModel.SchoolName, "School Updated", NotificationType.success);
					return RedirectToAction("AllSchools");
				}
                SweetAlert(viewModel.SchoolName, result, NotificationType.error);
                ModelState.AddModelError("message", result);
				return View(viewModel);
            }
			ModelState.AddModelError("message", "Please ensure you fill all fields");
			return View(viewModel);
		}
	
		public IActionResult UploadSchool()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> UploadSchool(IFormFile file)
		{

			if (file != null)
			{
				var res = await _schoolServices.UploadSchoolAsync(file);
				if (res== "Success")
				{
					SweetAlert(file.FileName,"Upload Successful",NotificationType.success);
					return RedirectToAction("AllSchools", "School");
				}
				TempData["UploadError"] = res;
				return View(file);
			}
			TempData["UploadError"] = "Select a File";
			return View(file);
		}

		public IActionResult ChangeSchoolStatus(int id)
		{
			var change = _schoolServices.ChangeStatus(id);
			SweetAlert(change.StatusName, change.SchoolName+" is "+ change.StatusName, NotificationType.info);
			return RedirectToAction("AllSchools");
		}

		public IActionResult RemoveSchool(int id)
		{
            try
            {
				_schoolServices.DeleteSchoolAsync(id);
                SweetAlert("Deleted", "School deleted", NotificationType.success);
                return RedirectToAction("AllSchools");
			}
            catch (Exception e)
            {
                SweetAlert("Error", e.Message, NotificationType.error);
                return RedirectToAction("AllSchools");
            }
        }
		public IActionResult AllInactiveSchool()
		{
			var schools = _schoolServices.GetSchools("inactive");
			return View(schools);
		}
	}
}