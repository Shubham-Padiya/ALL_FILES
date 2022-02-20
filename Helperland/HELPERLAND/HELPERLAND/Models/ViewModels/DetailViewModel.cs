using HELPERLAND.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HELPERLAND.Models.ViewModels
{
    public class DetailViewModel
    {
        public IEnumerable<UserAddress> userAddress { get; set; } = new List<UserAddress>();

        [Required(ErrorMessage = "Please provide service address with given postalcode")]
        public int? check { get; set; }
    }
}
