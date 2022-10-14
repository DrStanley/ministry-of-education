using System;
using EIMS.Domain.Entities;
using EIMS.Domain.Entities.Enum;

namespace EIMS.Models
{
    public class SchoolViewModel
    {
        public int Id { get; set; }
        
        public string SchoolName { get; set; }

        public string Email { get; set; }

        public string SchoolCode { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string LgaName { get; set; }

        public int LocalGovernmentId { get; set; }

        public LocalGovernment LocalGovernment { get; set; }

        public DateTime IncorporationDate { get; set; }

        public string PrincipalName { get; set; }

        public SchoolCategory Category { get; set; }

        public string CategoryName
        {
            get
            {
                switch (Category)
                {
                    case SchoolCategory.PRIMARY:
                        return "Primary";
                    case SchoolCategory.SECONDARY:
                        return "Secondary";
                    default:
                        return "Primary/Secondary";
                }
            }
        }


        public SchoolType Type { get; set; }

        public string TypeName
        {
            get
            {
                switch (Type)
                {
                    case SchoolType.PRIVATE:
                        return "Private";
                    case SchoolType.PUBLIC:
                        return "Public";
                    default:
                        return "Public";
                }
            }
        }

        public Status Status { get; set; }

        public string StatusName => Status == Status.Active ? "Active" : "Inactve";
    }
}
