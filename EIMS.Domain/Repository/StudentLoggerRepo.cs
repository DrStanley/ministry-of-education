using System;
using System.Collections.Generic;
using System.Text;
using EIMS.Domain.Data;
using EIMS.Domain.Entities;
using EIMS.Domain.Interfaces;

namespace EIMS.Domain.Repository
{
	public class StudentLoggerRepo: GenericRepo<StudentLog>, IStudentLoggerRepo
	{
		private readonly ApplicationDbContext _dbContext;
		public StudentLoggerRepo(ApplicationDbContext context)
			: base(context)
		{
			_dbContext = context;
		}
}
}
