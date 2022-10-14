using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EIMS.Domain.Data;
using EIMS.Domain.Entities;
using EIMS.Domain.Interfaces;

namespace EIMS.Domain.Repository
{
    public class ProfileRepo : GenericRepo<UserProfile>, IProfileRepo
    {
        private readonly ApplicationDbContext _dbContext;
        public ProfileRepo(ApplicationDbContext context)
            :base(context)
        {
            _dbContext = context;
        }

        public async Task<Tuple<int, UserProfile>> CreateProfile(string userId)
        {
            var profile = new UserProfile(){ UserId = userId };
            await _dbContext.Profiles.AddAsync(profile);
            var saved = await _dbContext.SaveChangesAsync();
            return new Tuple<int, UserProfile>(saved, profile);
        }
    }
}
