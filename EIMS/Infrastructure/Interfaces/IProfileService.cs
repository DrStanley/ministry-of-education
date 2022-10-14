using System.Threading.Tasks;
using EIMS.Models;
using Microsoft.AspNetCore.Identity;

namespace EIMS.Infrastructure.Interfaces
{
    public interface IProfileService
    {
        
        Task<IdentityResult> ChangePassword(string username, ChangePasswordViewModel changePasswordViewModel);
        Task<ProfileViewModel> GetUserProfile(string username);
        string UpdateBio(ProfileViewModel profileViewModel);
    }
}
