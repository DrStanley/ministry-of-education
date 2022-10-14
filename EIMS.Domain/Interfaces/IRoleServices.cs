using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EIMS.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace EIMS.Domain.Interfaces
{
	public interface IRoleServices
	{
		Task<IdentityResult> AssignRoleAsync(AppUser user, string roleId);
		Task<IdentityResult> CreateRoleAsync(string roleName);
		Task<IdentityResult> DeleteRoleAsync(string roleId);
		Task<IdentityRole> FindRoleByIdAsync(string id);
		Task<IdentityRole> FindRoleByNameAsync(string name);
		IEnumerable<IdentityRole> GetRoles();
		Task<IdentityResult> RemoveRoleAsync(AppUser user, string roleId);
		Task<IdentityResult> UpdateRoleAsync(string roleId, string newRoleName);
	}
}
