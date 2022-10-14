using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities.Enum;
using EIMS.Infrastructure.Interfaces;
using EIMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.Controllers
{
	public class SubjectController : BaseController
	{
		private readonly ISubjectServices _subjectServices;

		public SubjectController(ISubjectServices subjectServices)
		{
			_subjectServices = subjectServices;
		}

		public IActionResult AllSubjects()
		{
			return View( _subjectServices.GetAllSubjects().OrderBy(s=>s.SubjectName));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Add(string subjectView, SchoolCategory subjectLevel)
		{
			if (subjectView != null)
			{
				var res = _subjectServices.CreateSubject(subjectView, subjectLevel);
				if (res == "Success")
				{
					SweetAlert(subjectView, "Added Successfully", NotificationType.success);
				}
				else
				{
					SweetAlert("Error", res, NotificationType.error);
				}
			}
			else
			{
				SweetAlert("Error", "Class name is required", NotificationType.error);

			}

			return RedirectToAction("AllSubjects", "Subject");
		}

		public IActionResult GetSubject(int id)
		{
			return PartialView("_EditSubjectPartial", _subjectServices.GetAllSubjects()
				.FirstOrDefault(c => c.Id == id));
		}
		public IActionResult UpdateSubject(SubjectViewModel model)
		{
			if ((model.SubjectName != null || model.SubjectName == "") && model.Id != 0)
			{
				var res = _subjectServices.UpdateStudent(model);

				if (res == "Success")
				{
					SweetAlert(model.SubjectName, "Update Successfully", NotificationType.success);
				}
				else
				{
					SweetAlert("Error", res, NotificationType.success);

				}

			}

			return RedirectToAction("AllSubjects", "Subject");
		}

		[HttpPost]
		public IActionResult DeleteSubject(int id)
		{
			var res = _subjectServices.RemoveSubject(id);
			if (res == "Success")
			{
				SweetAlert("Class", "Removed Successfully", NotificationType.success);
			}
			else
			{
				SweetAlert("Error", res, NotificationType.error);
			}


			return RedirectToAction("AllSubjects", "Subject");
		}

	}
}
