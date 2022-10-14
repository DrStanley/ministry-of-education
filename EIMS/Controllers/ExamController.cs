using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities.Enum;
using EIMS.Infrastructure.Interfaces;
using EIMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace EIMS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ExamController : BaseController
    {
        private readonly IExamService _examService;
        private readonly IClassesServices _classService;

        public ExamController(IExamService examService, IClassesServices classesServices)
        {
            _examService = examService;
            _classService = classesServices;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddExam()
        {
            var examVm = _examService.InitExamViewModel();
            return View(examVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddExam(AddExamViewModel examViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = _examService.AddExam(examViewModel);
                if (result > 0)
                {
                    SweetAlert("Success", $"{examViewModel.ExaminationName} was successfully created", NotificationType.success);
                    return RedirectToAction("Index", "Exam");
                }
                ModelState.AddModelError("", "Failed to add exam, please try again");
            }

            return View(examViewModel);
        }

        public JsonResult GetExams()
        {
            try
            {

                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                string searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                // int recordsTotal = 0;


                var exams = _examService.GetAllExams();

                /*if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                }*/

                if (!string.IsNullOrEmpty(searchValue))
                {
                    exams = exams.Where(ex => ex.ExaminationName.Contains(searchValue));
                }
                var count = exams.Count();

                var data= exams.Skip(skip).Take(pageSize).ToList();

                // Paging Length 10,20  
                return Json(new { draw, recordsFiltered = count, recordsTotal = count, data });
            }
            catch
            {
                throw;
            }
        }

        public IActionResult EditExam(int id)
        {
            try
            {
                var editExam = _examService.GetExam(id);
                return View(editExam);
            }
            catch (Exception error)
            {
                SweetAlert("Error", error.Message, NotificationType.error);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditExam(ExamViewModel examViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = _examService.UpdateExam(examViewModel);
                if (result > 0)
                {
                    SweetAlert("Updated", $"{examViewModel.ExaminationCode} has been updated", NotificationType.success);
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError("", "Failed to update user");
            return View(examViewModel);
        }

        public IActionResult DeleteExam(int id)
        {
            try
            {
                var delExam = _examService.DeleteExam(id);
                SweetAlert("Deleted!", $"{delExam.ExaminationName} ({delExam.ExaminationCode}) has been removed",
                    NotificationType.info);
                return RedirectToAction("Index");
            }
            catch (Exception error)
            {
                SweetAlert("Error!", error.Message, NotificationType.error);
                return RedirectToAction("Index");
            }
        }

        public IActionResult ActivateExam(int id)
        {
            try
            {
                _examService.ActivateOrDeactivateExam(id, Status.Active);
                return RedirectToAction("Index");
            }
            catch (Exception error)
            {
                SweetAlert("Failed", error.Message, NotificationType.error);
                return RedirectToAction("Index");
            }
            
        }

        public IActionResult DeactivateExam(int id)
        {
            try
            {
                _examService.ActivateOrDeactivateExam(id, Status.Inactive);
                return RedirectToAction("Index");
            }
            catch (Exception error)
            {
                SweetAlert("Failed", error.Message, NotificationType.error);
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        public async Task<IActionResult> AddExamSubjects(int id)
        {
            var vm = await _examService.GetExamSubjects(id);
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddExamSubjects(AddSubjectsViewModel vm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var message = await _examService.AddSubjectsToExam(vm);
                    SweetAlert("Success", message, NotificationType.success);
                    return RedirectToAction("Index");
                }
                catch(Exception error)
                {
                    ModelState.AddModelError("", error.Message);
                }
            }
            return View(vm);
        }
    }
}
