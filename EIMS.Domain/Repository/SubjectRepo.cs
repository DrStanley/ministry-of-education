using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EIMS.Domain.Data;
using EIMS.Domain.Entities;
using EIMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Domain.Repository
{
	public class SubjectRepo : GenericRepo<Subject>, ISubjectRepo
	{
		private readonly ApplicationDbContext _dbcontext;

		public SubjectRepo(ApplicationDbContext context)
			: base(context)
		{
			_dbcontext = context;
		}

		public IEnumerable<Subject> GetSubjectsIncludeEnrolled()
		{
			return _dbcontext.Subjects.Include(s => s.OfferedExamStudents);
		}
    }
}
