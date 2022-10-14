using System.Threading.Tasks;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;

namespace EIMS.Domain.Interfaces
{
    public interface IExamRepo: IGenericRepo<Examination>
    {
        void ActivateOrDeactivateExam(int id, Status action);
        Task<Examination> GetExam(int id);
        Task<Examination> GetExamIncludeStudent(int id);
    }
}
