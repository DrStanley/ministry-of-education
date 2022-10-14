using System;
using System.Collections.Generic;
using System.Text;
using EIMS.Domain.Data;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;
using EIMS.Domain.Interfaces;

namespace EIMS.Domain.Repository
{
	public class TeacherLoggerRepo : GenericRepo<TeacherLog>, ITeacherLoggerRepo
	{
		private readonly ApplicationDbContext _dbContext;
		public TeacherLoggerRepo(ApplicationDbContext context)
			: base(context)
		{
			_dbContext = context;
		}

	}
}
