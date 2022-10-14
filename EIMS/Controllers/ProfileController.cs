using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;
using EIMS.Domain.Interfaces;
using EIMS.Infrastructure.Interfaces;
using EIMS.Infrastructure.PageHelpers;
using EIMS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace EIMS.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ITeacherRepo _teacherRepo;
        private readonly IProfileService _profileService;
        private readonly IAccountServices _accountServices;
        private readonly ISchoolService _schoolService;
        private readonly IQualificationRepo _qualificationRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileController(
            IMapper mapper,
            IProfileService profileService,
            ITeacherRepo teacherRepo,
            IAccountServices accountServices,
            ISchoolService schoolService,
            IHttpContextAccessor httpContextAccessor,
            IQualificationRepo qualificationRepo)
        {
            _mapper = mapper;
            _teacherRepo = teacherRepo;
            _profileService = profileService;
            _accountServices = accountServices;
            _schoolService = schoolService;
            _qualificationRepo = qualificationRepo;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> EditProfile()
        {
	        if (User.IsInRole("SchoolAdmin"))
	        {
		        var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;

		        var sId = _schoolService.GetSchools().Where(model => model.Email == email).Select(model => model.Id).FirstOrDefault();
		        return RedirectToAction("EditSchool", "School", new { id = sId });


	        }

            var profileVm = await _profileService.GetUserProfile(User.Identity.Name);
            return View(profileVm);
        }


        public async Task<ActionResult> ChangeProfile()
        {
            var profileVm = await _profileService.GetUserProfile(User.Identity.Name);
            return View(profileVm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeProfile(ProfileViewModel pViewModel)
        {
            var user = await _accountServices.FindUser(User.Identity.Name);
            pViewModel.User = user;
            pViewModel.Teacher = await _teacherRepo.GetTeacherDetails(t => t.UserId == user.Id);
            pViewModel.TeacherId = pViewModel.Teacher.Id;
            if (ModelState.IsValid)
            {
                var result = "";
                switch (pViewModel.UpdateType)
                {
                    case "bio":
                        result = _profileService.UpdateBio(pViewModel);
                        break;
                }
                if (result == "success")
                {
                    SweetAlert("Success", "Profile was successfully updated", NotificationType.success);
                    return RedirectToAction("EditProfile");
                }
            }
            ModelState.AddModelError("", "Please ensure you fill the required fields");
            return RedirectToAction("EditProfile");
        }


        public IActionResult GetQualifications(int teacherId)
        {
            var qualifications = _qualificationRepo.Find(q => q.TeacherId == teacherId);
            return PartialView("_Qualifications", qualifications);
        }

        [HttpGet]
        public IActionResult AddOrEditQualification(int teacherId, int id = 0)
        {
            if (id != 0)
            {
                var qualModel = _mapper.Map<QualificationViewModel>(_qualificationRepo.GetById(id));
                return View(qualModel);
            }
            var addExpModel = new QualificationViewModel() { TeacherId = teacherId };
            return View(addExpModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddOrEditQualification(int id, QualificationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var qualification = _mapper.Map<Qualification>(viewModel);
                if (id == 0)
                {
                    _qualificationRepo.Insert(qualification);
                }
                else
                {
                    _qualificationRepo.Update(qualification);
                }

                var result = _qualificationRepo.Save();
                if (result > 0)
                {
                    var message = id == 0 ? "Added new qualification" : "Changes have been saved";
                    SweetAlert("Success", message, NotificationType.success);
                    return new RedirectResult(Url.Action("EditProfile")+"#qualifications");
                }
            }

            var failureMessage = id == 0 ? "add" : "edit";
            SweetAlert("Error", $"Failed to {failureMessage} qualification", NotificationType.error);
            return new RedirectResult(Url.Action("EditProfile")+"#qualifications");
        }

        public IActionResult DeleteQualification(int id)
        {
            var qualification = _qualificationRepo.GetById(id);
            if (qualification != null)
            {
                _qualificationRepo.Delete(id);
                var result = _qualificationRepo.Save();
                if(result > 0)
                {
                    SweetAlert("Done!", "Qualification has been deleted", NotificationType.info);
                    return new RedirectResult(Url.Action("EditProfile") + "#qualifications");
                }

            }
            SweetAlert("Error", $"Failed to delete qualification", NotificationType.error);
            return new RedirectResult(Url.Action("EditProfile") + "#qualifications");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel cpViewModel)
        {
            if (ModelState.IsValid)
            {
                var username = User.Identity.Name;
                var result = await _profileService.ChangePassword(username, cpViewModel);
                if (result.Succeeded)
                {
                    TempData["Alert"] = "Password has been changed";
                    if (User.IsInRole("Teacher"))
                    {
                        return RedirectToAction("EditProfile");
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Password change failed, try again");
            return View();
        }
    }
}
