using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Domain.Entities.Enum;
using EIMS.Domain.Interfaces;
using EIMS.Infrastructure.Interfaces;
using EIMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.Controllers
{
	[Authorize]
	public class InspectorController : BaseController
	{
		private readonly IInspectorServices _inspectorServices;
		private readonly ILocalGovernmentRepo _localGovernmentRepo;
		private readonly IMapper _mapper;
		private ISchoolService _schoolService;

		public InspectorController(IMapper mapper, 
			IInspectorServices inspectorServices,
			ILocalGovernmentRepo localGovernmentRepo,
			ISchoolService schoolService)
		{
			_mapper = mapper;
			_inspectorServices = inspectorServices;
			_localGovernmentRepo = localGovernmentRepo;
			_schoolService = schoolService;
		}

		public IActionResult AllInspectors()
		{
			return View(_inspectorServices.GetInspectors());
		}

		public IActionResult AddInspector()
		{
			var local = new CreateInspectorViewModel() {LocalGovernments = _localGovernmentRepo.GetAll()};
			return View(local);
		}

		[HttpPost]
		public async Task<IActionResult> AddInspector(CreateInspectorViewModel model)
		{
			model.LocalGovernments = _localGovernmentRepo.GetAll();

			if (ModelState.IsValid)
			{
				var result = await _inspectorServices.CreateInspector(model);
				if (result == "Success")
				{
					SweetAlert("Success", $"{model.Name} has been added", NotificationType.success);
					return RedirectToAction("AllInspectors", "Inspector");
				}

				ModelState.AddModelError("", result);
				return View(model);
			}
			ModelState.AddModelError("", "Invalid data");
			return View(model);
		}

		public IActionResult EditInspector(int id)
		{
			var model = _mapper.Map<CreateInspectorViewModel>(_inspectorServices.GetInspectors()
				.FirstOrDefault(t => t.Id == id));
			model.LocalGovernments = _localGovernmentRepo.GetAll();
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		 public async Task<IActionResult> EditInspector(CreateInspectorViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				var result = await _inspectorServices.UpdateInspector(viewModel);
				if (result == "Success")
				{
					SweetAlert("Success", $"{viewModel.Name} has been Updated", NotificationType.success);
					return RedirectToAction("AllInspectors", "Inspector");
				}

				ModelState.AddModelError("", result);
				return View(viewModel);
			}
			ModelState.AddModelError("", "Invalid data");
			return View(viewModel);
		}

		public IActionResult ChangeInspectorStatus(int id)
		{
			var change = _inspectorServices.ChangeInspectorStatus(id);
			var stat = Enum.GetName(typeof(Status), change.Status);

			SweetAlert(stat.ToUpper(), "Inspector status is " + stat, NotificationType.success);
			return RedirectToAction("AllInspectors");
		}

		public IActionResult DeleteInspector(int id)
		{
			_inspectorServices.DeleteInspector(id);
			SweetAlert("Deleted", "Inspector deleted successful", NotificationType.success);

			return RedirectToAction("AllInspectors");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult AssignInspector(InspectorViewModel viewModel)
		{
			if (viewModel.LocalGovernmentId!=null && viewModel.LocalGovernmentId!=0)
			{
				var res = _inspectorServices.AssignInspector(viewModel.Id,(int)viewModel.LocalGovernmentId);
				if (res == "Success")
				{
					SweetAlert("Assigned", viewModel.Name + " has been Assigned", NotificationType.success);
					return RedirectToAction("AllInspectors");
				}
		    	SweetAlert("Error", res, NotificationType.error);
		 	}
			return RedirectToAction("AllInspectors");
		}

		public IActionResult GetInspectorPartial(int id)
		{
			ViewBag.Lga = _localGovernmentRepo.GetAll();
			return PartialView("_AssignInspectorPartial", _inspectorServices.GetInspectors()
				.FirstOrDefault(c => c.Id == id));
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult TransferInspector(TransferInspectorViewModel viewModel)
		{
			if (viewModel.LocalGovernmentIdTo != 0)
			{
				var res = _inspectorServices.TransferInspector(viewModel);
				if (res == "Success")
				{
					SweetAlert("Transfered", viewModel.Name + " has been Transferred", NotificationType.success);
					return RedirectToAction("AllInspectors");
				}
				SweetAlert("Error", res, NotificationType.error);
			}
			return RedirectToAction("AllInspectors");
		}

		public IActionResult GetInspectorTransferPartial(int id)
		{
			ViewBag.Lga = _localGovernmentRepo.GetAll();
			return PartialView("_TransferInspectorPartial", _mapper.Map<TransferInspectorViewModel>(_inspectorServices.GetInspectors()
				.FirstOrDefault(c => c.Id == id)));
		}
		
		public IActionResult UnAssignInspector(int id)
		{
			var res = _inspectorServices.UnAssignInspector(id);
			if (res == "Success")
			{
				SweetAlert("Unassigned", "Inspector has been unassigned", NotificationType.info);
				return RedirectToAction("AllInspectors");
			}
			SweetAlert("Error", res, NotificationType.error);
			return RedirectToAction("AllInspectors");
		}

		public JsonResult CheckLga(int id)
		{
			return Json(_inspectorServices.IsLgaAssigned(id));
		}

		public IActionResult SchoolInspector(string mail)
		{
			var school = _schoolService.GetSchools().FirstOrDefault(s => s.Email == mail);
			var model = _inspectorServices.GetInspectors()
				.FirstOrDefault(i => i.LocalGovernmentId == school.LocalGovernmentId);
			return View(model);
		}
	}

}
