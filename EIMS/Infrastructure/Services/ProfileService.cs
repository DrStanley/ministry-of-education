using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Domain.Entities;
using EIMS.Domain.Interfaces;
using EIMS.Infrastructure.Interfaces;
using EIMS.Models;
using Microsoft.AspNetCore.Identity;

namespace EIMS.Infrastructure.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IAccountServices _accountServices;
        private readonly IProfileRepo _profileRepo;
        private readonly IMapper _mapper;
        private readonly IQualificationRepo _qualificationRepo;
        private readonly ITeacherRepo _teacherRepo;

        public ProfileService(
            IAccountServices accountServices, 
            IProfileRepo profileRepo, 
            ITeacherRepo teacherRepo, 
            IQualificationRepo qualificationRepo,
            IMapper mapper)
        {
            _accountServices = accountServices;
            _profileRepo = profileRepo;
            _teacherRepo = teacherRepo;
            _mapper = mapper;
            _qualificationRepo = qualificationRepo;
        }

        public async Task<IdentityResult> ChangePassword(string username,
            ChangePasswordViewModel changePasswordViewModel)
        {
            return await _accountServices.ChangePassword(username, changePasswordViewModel.CurrentPassword,
                changePasswordViewModel.NewPassword);
        }

        public async Task<ProfileViewModel> GetUserProfile(string username)
        {
            var user = await _accountServices.FindUser(username);
            if (user != null)
            {
                var userId = user.Id;
                var profile = _mapper.Map<ProfileViewModel>(_profileRepo.FindOne(c => c.UserId == userId));
                if (profile != null)
                {
                    profile.User = user;
                    profile.User = user;
                    var teacher = await _teacherRepo.GetTeacherDetails(t => t.UserId == user.Id);
                    if (teacher != null)
                    {
                        profile.Teacher = teacher;
                        profile.TeacherId = teacher.Id;
                    }

                    profile.Teacher.Qualifications = _qualificationRepo.Find(q => q.TeacherId == profile.TeacherId).ToList();
                    return profile;
                }
                throw new Exception("User profile can not be found");
            }
            throw new Exception("User does not exist");
            
        }

        public string UpdateBio(ProfileViewModel profileViewModel)
        {
            try
            {
                var targetProfile = _profileRepo.GetById(profileViewModel.Id);
                targetProfile.Address = profileViewModel.Address;
                targetProfile.Gender = profileViewModel.Gender;
                targetProfile.StateOfOrigin = profileViewModel.StateOfOrigin;
                targetProfile.Dob = profileViewModel.Dob;
                _profileRepo.Update(targetProfile);
                var result = _profileRepo.Save();
                if (result > 0)
                {
                    return "success";
                }
                return "fail";
            }
            catch
            {
                return "fail";
            }
            

        }
    }
}
