using System;
using System.Collections.Generic;
using System.Text;
using EIMS.Domain.Entities;

namespace EIMS.Domain.Interfaces
{
	public interface ISubjectRepo : IGenericRepo<Subject>
	{
		IEnumerable<Subject> GetSubjectsIncludeEnrolled();
	}
}
