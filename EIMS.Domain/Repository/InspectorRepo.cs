using System;
using System.Collections.Generic;
using System.Text;
using EIMS.Domain.Data;
using EIMS.Domain.Entities;
using EIMS.Domain.Interfaces;

namespace EIMS.Domain.Repository
{
	public class InspectorRepo : GenericRepo<Inspector>, IInspectorRepo
	{
		private readonly ApplicationDbContext _dbContext;
		public InspectorRepo(ApplicationDbContext context)
			: base(context)
		{
			_dbContext = context;
		}

		public void DeleteInspector(int id)
		{
		//	var tran = _dbContext.Database.BeginTransaction();
			var inspector = _dbContext.Inspectors.Find(id);
			if (inspector != null)
			{
				var user = _dbContext.Users.Find(inspector.UserId);
				_dbContext.Users.Remove(user);
				_dbContext.Inspectors.Remove(inspector);
				_dbContext.SaveChanges();
			}
		}
	}
}
