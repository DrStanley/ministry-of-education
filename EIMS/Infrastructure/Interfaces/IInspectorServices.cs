using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Models;

namespace EIMS.Infrastructure.Interfaces
{
	public interface IInspectorServices
	{
		Task<string> CreateInspector(CreateInspectorViewModel inspectorViewModel);
		Task<string> UpdateInspector(CreateInspectorViewModel inspectorViewModel);
		IEnumerable<InspectorViewModel> GetInspectors();
		InspectorViewModel ChangeInspectorStatus(int id);
		void DeleteInspector(int id);
		string UnAssignInspector(int id);
		string AssignInspector(int id, int lgaId);
		string TransferInspector(TransferInspectorViewModel transferInspectorViewModel);
		bool IsLgaAssigned(int localGovernmentId);
	}
}
