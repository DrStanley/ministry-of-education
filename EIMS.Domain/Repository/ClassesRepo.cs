using System;
using System.Collections.Generic;
using System.Text;
using EIMS.Domain.Data;
using EIMS.Domain.Entities;
using EIMS.Domain.Interfaces;

namespace EIMS.Domain.Repository
{
	public class ClassesRepo : GenericRepo<Classes>, IClassesRepo
	{
		private readonly ApplicationDbContext _dbContext;
		public ClassesRepo(ApplicationDbContext context)
			: base(context)
		{
			_dbContext = context;
		}
	}
}
