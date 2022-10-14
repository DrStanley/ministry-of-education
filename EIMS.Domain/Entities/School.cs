using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using EIMS.Domain.Entities.Enum;
using EIMS.Domain.Identity;
using EIMS.Domain.Repository;

namespace EIMS.Domain.Entities
{
	public class School
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

        public string UserId { get; set; }
		public AppUser User { get; set; }

		[Required(ErrorMessage = "School Name needed")]
		public string SchoolName { get; set; }

		[EmailAddress]
		[Required(ErrorMessage = "School Email needed")]
		public string Email { get; set; }

		[Required(ErrorMessage = "School Code needed")]
		public string SchoolCode { get; set; }

		[Required(ErrorMessage = "School Number needed")]
		public string PhoneNumber { get; set; }

		[Required(ErrorMessage = "School Address needed")]
		public string Address { get; set; }

        public int LocalGovernmentId { get; set; }

        public LocalGovernment LocalGovernment { get; set; }

		[Required(ErrorMessage = "School Date Of Incorporation needed")]
		public DateTime IncorporationDate { get; set; }

        [Required(ErrorMessage = "Principal Name needed")]
		public string PrincipalName { get; set; }

		[Required(ErrorMessage = "School Category needed")]
		public SchoolCategory SchoolCategory { get; set; }

		[Required(ErrorMessage = "School Category needed")]
		public SchoolType SchoolType { get; set; }

		public ICollection<Student> Students { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.Now;
		public Status Status { get; set; } = Status.Active;

	}
}
