using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities.Enum;
using EIMS.Domain.Interfaces;
using EIMS.Infrastructure.Interfaces;
using EIMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace EIMS.Controllers
{
    [Authorize(Roles = "SchoolAdmin")]
    public class EnrollStudentsController : BaseController
    {
        private readonly IExamEnrollmentService _examEnrollmentService;

        public EnrollStudentsController(IExamEnrollmentService examEnrollmentService)
        {
            _examEnrollmentService = examEnrollmentService;
        }
        public IActionResult Index()
        {
            var exams = _examEnrollmentService.GetAccreditedExams(User.Identity.Name);
            return View(exams);
        }

        [HttpGet]
        public async Task<IActionResult> AddExamStudents(int id)
        {
            try
            {
                var examStudentsViewModel = await _examEnrollmentService.GetExamExamStudents(id);
                return View(examStudentsViewModel);
            }
            catch (Exception error)
            {
                SweetAlert("Failed", error.Message, NotificationType.warning);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddExamStudents(int id, AddExamStudentsViewModel addExamStudentsViewModel)
        {
            if (ModelState.IsValid)
            {
                var (result, message) = await _examEnrollmentService.EnrollStudentsForExam(id, addExamStudentsViewModel);
                if (result > 0)
                {
                    SweetAlert("Success!", message, NotificationType.success);
                    return RedirectToAction("OfferStudentSubjects", new { id });
                }
                ModelState.AddModelError("", message+ "try again");
                return View(addExamStudentsViewModel);
            }
            ModelState.AddModelError("", "Please fill all required fields");
            return View(addExamStudentsViewModel);
        }

        public async Task<IActionResult> OfferStudentSubjects(int id)
        {
	        var examSubjects = await _examEnrollmentService.GetExamSubjects(id);
	        return View(examSubjects);
        }

        public IActionResult AssignToSubject(int id, int examId)
        {
	        var vm = _examEnrollmentService.GetSubjectStudents(examId, id);
	        return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignToSubject(AssignSubjectStudentsViewModel viewModel)
        {
	        if (ModelState.IsValid)
	        { 
		        var (result, message) = _examEnrollmentService.AssignSubjectCandidates(viewModel);
		        if (result > 0)
		        {
			        SweetAlert("Success", message, NotificationType.success);
			        return RedirectToAction("OfferStudentSubjects", new {id = viewModel.ExamId});
		        }

		        SweetAlert("Error", message, NotificationType.error);
		        return View(viewModel);

	        }

	        ModelState.AddModelError("", "Please fill the required fields");
	        return View(viewModel);
        }
    }
}
