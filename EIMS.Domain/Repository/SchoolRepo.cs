using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EIMS.Domain.Data;
using EIMS.Domain.Entities;
using EIMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Domain.Repository
{
	public class SchoolRepo : ISchoolRepo
	{
		private readonly ApplicationDbContext _dbContext;

		public SchoolRepo(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public int CreateSchool(School school, string userId)
		{
			school.UserId = userId;
			_dbContext.Schools.Add(school);
			return _dbContext.SaveChanges();
		}

		public int UpdateSchool(School updateSchool)
		{
			var targetSchool = _dbContext.Schools.Find(updateSchool.Id);
			if (targetSchool != null)
			{
				updateSchool.Status = targetSchool.Status;
				updateSchool.User = targetSchool.User;
				updateSchool.UserId = targetSchool.UserId;
				updateSchool.CreatedAt = targetSchool.CreatedAt;
				_dbContext.Entry(targetSchool).CurrentValues.SetValues(updateSchool);
			}
			return _dbContext.SaveChanges();
		}

		public DbContext GetDbContext()
		{
			return _dbContext;
		}

		public async Task<IEnumerable<School>> GetAllSchoolsAsync()
		{
			return await _dbContext.Schools.Include(school => school.LocalGovernment)
			.OrderBy(s => s.CreatedAt).ToListAsync();
		}

		public School FindSchool(int id)
		{
			return _dbContext.Schools.SingleOrDefault(s => s.Id == id);
		}
		public School FindSchoolEmail(string email)
		{
			return _dbContext.Schools.SingleOrDefault(s => s.Email == email);
		}

		public async Task<School> FindSchoolIncludeStudents(string email)
        {
			return await _dbContext.Schools.Include(s => s.Students).FirstAsync(s => s.Email == email);
        }

		public IEnumerable<School> GetAllSchoolByLga(int lgaId)
		{
			return _dbContext.Schools.Where(d => d.LocalGovernmentId == lgaId).ToList();
		}

		public void DeleteSchool(int id)
		{
			var school = _dbContext.Schools.Find(id);
			var user = _dbContext.Users.Find(school.UserId);
			_dbContext.Users.Remove(user);
			_dbContext.Schools.Remove(school);
			_dbContext.SaveChanges();
		}
	}
}
