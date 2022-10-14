using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EIMS.Models
{
    public class AddExamSubjectsViewModel
    {
        [Required] 
        public int Id { get; set; }

        [Editable(false)]
        [ReadOnly(true)]
        public string ExaminationName { get; set; }
        public MultiSelectList Subjects{ get; set; }
        public int[] SelectedSubjects { get; set; }
    }
}
