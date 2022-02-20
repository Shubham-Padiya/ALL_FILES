using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HELPERLAND.Models.ViewModels
{
    public class SchedualViewModel
    {
        public DateTime ServiceStartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime ServiceDate { get; set; }

        [DataType(DataType.Time)]
        public DateTime ServiceTime { get; set; }
        public double ServiceHours { get; set; }
        public string? Comments { get; set; }

        [Display(Name =" I have pets at Home")]
        public bool HasPets { get; set; }
    }
}
