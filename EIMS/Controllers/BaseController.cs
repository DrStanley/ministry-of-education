using System;
using EIMS.Domain.Entities.Enum;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.Controllers
{
	public abstract class BaseController : Controller
	{
		public void SweetAlert(string title,string message, NotificationType notificationType)
		{
			TempData["notificationType"] = Enum.GetName(typeof(NotificationType), notificationType);
			TempData["notificationTitle"] = title;
			TempData["notificationMessage"] = message;
		}

		
	}
}
