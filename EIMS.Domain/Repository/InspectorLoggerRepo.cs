using System;
using System.Collections.Generic;
using System.Text;
using EIMS.Domain.Data;
using EIMS.Domain.Entities;
using EIMS.Domain.Interfaces;

namespace EIMS.Domain.Repository
{
	public class InspectorLoggerRepo : GenericRepo<InspectorLog>, IInspectorLoggerRepo
	{
		private readonly ApplicationDbContext _dbContext;
		public InspectorLoggerRepo(ApplicationDbContext context)
			: base(context)
		{
			_dbContext = context;
		}

	}
}
