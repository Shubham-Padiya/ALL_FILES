using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HELPERLAND.Models.ViewModels
{
    public class ZipCodeViewModel
    {
        [Required]
        public string ZipCode { get; set; }
    }
}
