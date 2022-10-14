using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EIMS.Domain.Entities;

namespace EIMS.Domain.Interfaces
{
    public interface ITeacherRepo : IGenericRepo<Teacher>
    {
	    void DeleteTeachers(int id);
	    IEnumerable<School> RetSchools();
	    IEnumerable<LocalGovernment> RetLGA();
        Task<Teacher> GetTeacherDetails(Expression<Func<Teacher, bool>> predicate);
    }
}
