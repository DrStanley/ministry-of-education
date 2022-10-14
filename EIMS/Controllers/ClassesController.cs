using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities.Enum;
using EIMS.Infrastructure.Interfaces;
using EIMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.Controllers
{
	[Authorize(Roles = "Admin")]
	public class ClassesController : BaseController
	{
		private readonly IClassesServices _classesServices;

		public ClassesController(IClassesServices classesServices)
		{
			_classesServices = classesServices;
		}
		public IActionResult AllClasses()
		{
			return View(_classesServices.GetAllClasses().OrderBy(cl => cl.ClassName));
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Add(string classView, ClassType classType)
		{
			if (classView != null& classType!=0)
			{
				var res = _classesServices.CreateClass(classView, classType);
				if (res == "Success")
				{
					SweetAlert(classView, "Added Successfully", NotificationType.success);
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

			return RedirectToAction("AllClasses", "Classes");
		}

		[HttpPost]
		public IActionResult DeleteClass(int id)
		{
			var res = _classesServices.RemoveClass(id);
			if (res == "Success")
			{
				SweetAlert("Class", "Removed Successfully", NotificationType.success);
			}
			else
			{
				SweetAlert("Error", res, NotificationType.error);
			}


			return RedirectToAction("AllClasses", "Classes");
		}

		public IActionResult GetClass(int id)
		{
			return PartialView("_EditClassPartial", _classesServices.GetAllClasses()
				.FirstOrDefault(c => c.Id == id));
		}
		public IActionResult UpdateClass(ClassViewModel model)
		{
			if ((model.ClassName != null || model.ClassName == "") && model.Id != 0)
			{
				var res = _classesServices.UpdateClass(model);

				if (res == "Success")
				{
					SweetAlert(model.ClassName, "Update Successfully", NotificationType.success);
				}
				else
				{
					SweetAlert("Error", res, NotificationType.success);

				}

			}

			return RedirectToAction("AllClasses", "Classes");
		}
	}
}
