using System.Collections.Generic;
using EIMS.Domain.Entities.Enum;
using EIMS.Models;

namespace EIMS.Infrastructure.Interfaces
{
	public interface IClassesServices
	{
		IEnumerable<ClassViewModel> GetAllClasses(string query=null);
		string CreateClass(string className,ClassType classType);
		string RemoveClass(int id);
		string UpdateClass(ClassViewModel viewModel);
	}
}
