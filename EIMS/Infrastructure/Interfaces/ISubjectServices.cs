using System.Collections.Generic;
using EIMS.Domain.Entities.Enum;
using EIMS.Models;

namespace EIMS.Infrastructure.Interfaces
{
	public interface ISubjectServices
	{
		string CreateSubject(string subjectName, SchoolCategory subjectLevel);
		IEnumerable<SubjectViewModel> GetAllSubjects();
		string UpdateStudent(SubjectViewModel viewModel);
		string RemoveSubject(int id);
	}
}
