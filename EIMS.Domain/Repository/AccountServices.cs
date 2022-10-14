using System.Threading.Tasks;
using EIMS.Domain.Identity;
using EIMS.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EIMS.Domain.Repository
{
	public class AccountServices : IAccountServices
	{
		private readonly UserManager<AppUser> _userManager;

		public AccountServices(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<IdentityResult> CreateUser(AppUser user)
        {
            var password = $"{user.Name.Split(" ")[0]}@A123";
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> UpdateUser(AppUser profile)
        {
            var user = await _userManager.FindByEmailAsync(profile.Email);
            user.Name = profile.Name;
            user.PhoneNumber = profile.PhoneNumber;
            user.Title = profile.Title;
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ChangePassword(string username, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(username);
            var cpResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

            //If it fails, it returns the failed result
            if (!cpResult.Succeeded)
            {
                return cpResult;
            }
            // If it passes it tries to update the user's rows
            user.EmailConfirmed = true;
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteUser(string id)
        {
	        var user = await _userManager.FindByIdAsync(id);
			return await _userManager.DeleteAsync(user);
        }

        public async Task<AppUser> FindUser(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }
    }
}
