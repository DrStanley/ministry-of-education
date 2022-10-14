using EIMS.Domain.Data;
using EIMS.Domain.Entities;
using EIMS.Domain.Interfaces;

namespace EIMS.Domain.Repository
{
    public class QualificationRepo : GenericRepo<Qualification>, IQualificationRepo
    {
        public QualificationRepo(ApplicationDbContext context)
        : base(context)
        {
        }
    }
}
