using System;
using Microsoft.AspNetCore.Mvc;
using EIMS.Domain.Entities.Enum;
using EIMS.Infrastructure.Interfaces;
using EIMS.Models;

namespace EIMS.Controllers
{
    public class WorkExperienceController : BaseController
    {
        private readonly IWorkExperienceService _workExpService;

        public WorkExperienceController(IWorkExperienceService workExperienceService)
        {
            _workExpService = workExperienceService;
        }
        public IActionResult GetWorkExperience(int teacherId)
        {
            var experiences = _workExpService.GetTeacherWorkExperiences(teacherId);
            return PartialView("_WorkXp", experiences);
        }

        public IActionResult AddorEditExperience(int teacherId, int id = 0)
        {
            if (id != 0)
            {
                var workExpModel = _workExpService.GetWorkExperience(id);
                return View(workExpModel);
            }
            var addExpModel = new WorkExperienceViewModel() { TeacherId = teacherId };
            return View(addExpModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddorEditExperience(int id, WorkExperienceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var (result, message) = _workExpService.AddOrUpdateWorkExperience(viewModel, id);
                if (result > 0)
                {
                    SweetAlert("Success", message, NotificationType.success);
                    return RouteToWorkExperience();
                }
                SweetAlert("Error", message, NotificationType.error);
                return RouteToWorkExperience();
            }
            SweetAlert("Error", $"Please ensure you fill all required fields", NotificationType.error);
            return RouteToWorkExperience();
        }

        public IActionResult DeleteExperience(int id)
        {
            try
            {
                var (result, message) = _workExpService.DeleteWorkExperience(id);
                SweetAlert("Done!", message, NotificationType.info);
                return RouteToWorkExperience();
            }
            catch (Exception error)
            {
                SweetAlert("Error", error.Message, NotificationType.error);
                return RouteToWorkExperience();
            }
        }

        private IActionResult RouteToWorkExperience()
        {
            return new RedirectResult(Url.Action("EditProfile", "Profile") + "#profile1");
        }
    }
}
