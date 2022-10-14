using System;
using System.Collections.Generic;
using System.Text;
using EIMS.Domain.Data;
using EIMS.Domain.Entities;
using EIMS.Domain.Interfaces;

namespace EIMS.Domain.Repository
{
    public class LocalGovernmentRepo : GenericRepo<LocalGovernment>, ILocalGovernmentRepo
    {
        public LocalGovernmentRepo(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
