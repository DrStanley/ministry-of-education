using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EIMS.Domain.Identity;
using EIMS.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EIMS.Domain.Repository
{
	public class RoleServices : IRoleServices
	{
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public RoleServices(UserManager<AppUser> userMgr,RoleManager<IdentityRole> roleMgr)
        {
            _roleManager = roleMgr;
            _userManager = userMgr;
        }

        /// <summary>
        /// Gets a the roles
        /// </summary>
        /// <returns>Returns all roles</returns>
        public IEnumerable<IdentityRole> GetRoles()
        {
            return _roleManager.Roles;
        }

        /// <summary>
        /// Creates a role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole()
                {
                    Name = roleName,
                };

                return await _roleManager.CreateAsync(role);
            }
            return IdentityResult.Failed();
        }

        /// <summary>
        /// Deletes a role
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IdentityResult> DeleteRoleAsync(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                return await _roleManager.DeleteAsync(role);
            }
            return IdentityResult.Failed();
        }

        /// <summary>
        /// Assigns a user to a role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IdentityResult> AssignRoleAsync(AppUser user, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                return await _userManager.AddToRoleAsync(user, role.Name);
            }
            return IdentityResult.Failed();
        }

        /// <summary>
        /// Removes a user from a role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IdentityResult> RemoveRoleAsync(AppUser user, string roleId)
        {
            var role =await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                return await _userManager.RemoveFromRoleAsync(user, role.Name);
            }
            return IdentityResult.Failed();
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="newRoleName"></param>
        /// <returns></returns>
        public async Task<IdentityResult> UpdateRoleAsync(string roleId, string newRoleName)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                return await _roleManager.UpdateAsync(role);
            }
            return IdentityResult.Failed();
        }
        public async Task<IdentityRole> FindRoleByIdAsync(string id)
        {
            return await _roleManager.FindByIdAsync(id);
        }
        public async Task<IdentityRole> FindRoleByNameAsync(string name)
        {
            return await _roleManager.FindByNameAsync(name);
        }
    }
}
