using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EIMS.Domain.Entities.Enum;

namespace EIMS.Domain.Entities
{
    public class Qualification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string SchoolName { get; set; }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public EducationLevel EducationLevel { get; set; }
    }
}
