using System;
using System.Collections.Generic;
using System.Text;

namespace EIMS.Domain.Entities
{
	public class SubjectClasses
	{
		public int SubjectId { get; set; }
		public Subject Subject { get; set; }
		public int ClassesId { get; set; }
		public Classes Classes { get; set; }
	}
}
