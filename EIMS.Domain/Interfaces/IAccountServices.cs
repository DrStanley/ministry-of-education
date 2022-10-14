using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EIMS.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace EIMS.Domain.Interfaces
{
	public interface IAccountServices
    {
        Task<AppUser> FindUser(string username);
        Task<IdentityResult> CreateUser(AppUser user);
        Task<IdentityResult> UpdateUser(AppUser profile);
        Task<IdentityResult> ChangePassword(string username, string currentPassword, string newPassword);
        Task<IdentityResult> DeleteUser(string userId);

    }
}
