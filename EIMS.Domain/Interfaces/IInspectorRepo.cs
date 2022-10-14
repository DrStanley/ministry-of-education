using System;
using System.Collections.Generic;
using System.Text;
using EIMS.Domain.Entities;

namespace EIMS.Domain.Interfaces
{
	public interface IInspectorRepo : IGenericRepo<Inspector>
	{
		void DeleteInspector(int id);
	}
}
