using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EIMS.Domain.Entities;

namespace EIMS.Domain.Interfaces
{

    public interface IProfileRepo : IGenericRepo<UserProfile>
    {
        Task<Tuple<int, UserProfile>> CreateProfile(string userId);
    }
}
