using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EIMS.Domain.Data;
using EIMS.Domain.Entities;
using EIMS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Domain.Repository
{
    public class TeacherRepo : GenericRepo<Teacher>, ITeacherRepo
    {
	    private readonly ApplicationDbContext _dbContext;
	    public TeacherRepo(ApplicationDbContext context)
        :base(context)
        {
	        _dbContext = context;
        }

	    public IEnumerable<School> RetSchools()
	    {
			return  _dbContext.Schools
				.OrderBy(s => s.Id);
		}
	    public IEnumerable<LocalGovernment> RetLGA()
	    {
			return  _dbContext.LocalGovernments
				.OrderBy(s => s.Id).ToList();
		}
        public void DeleteTeachers(int id)
        {
	        var teacher = _dbContext.Teachers.Find(id);
	        if (teacher!=null)
	        {
		        var user = _dbContext.Users.Find(teacher.UserId);
		        var profile = _dbContext.Profiles.Find(teacher.ProfileId);
		        _dbContext.Users.Remove(user);
		        _dbContext.Teachers.Remove(teacher);
		        _dbContext.Profiles.Remove(profile);
		        _dbContext.SaveChanges();
	        }
        }

        public async Task<Teacher> GetTeacherDetails(Expression<Func<Teacher, bool>> predicate)
        {
            var teacher = await _dbContext.Teachers.Include(t => t.WorkExperiences).FirstAsync(predicate);
			return teacher;
        }
    }
}
