using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HELPERLAND.Models.ViewModels
{
    public class EditAddressViewModel
    {
        public int? AddressId { get; set; }

        [Required]
        [Display(Name = "Street name")]
        public string StreetName { get; set; }

        [Required]
        [Display(Name = "House number")]
        public string HouseNumber { get; set; }

        [Required]
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
    }
}
