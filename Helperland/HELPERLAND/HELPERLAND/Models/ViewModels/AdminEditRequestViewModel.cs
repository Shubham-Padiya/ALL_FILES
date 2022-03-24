using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HELPERLAND.Models.ViewModels
{
    public class AdminEditRequestViewModel
    {
        public int ServiceRequestId { get; set; }

        [Required(ErrorMessage = "Date Required")]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime ServiceStartDate { get; set; }

        [Required(ErrorMessage = "Time Required")]
        [Display(Name = "Time")]
        public string ServiceStartTime { get; set; }

        [Required]
        [Display(Name = "Street name")]
        public string StreetName { get; set; }

        [Required]
        [Display(Name = "House number")]
        public string HouseNumber { get; set; }

        [Display(Name = "Postal code")]
        [RegularExpression("^[0-9]{6}$", ErrorMessage = "Invalid ZipCode!!")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Why do you want to reschedule service request?")]
        public string? RescheduleReason { get; set; }


        public List<SelectListItem> timeList = new List<SelectListItem>{
                             new SelectListItem{Text="8:00", Value="8:00:00"},
                             new SelectListItem{Text="8:30", Value="8:30:00"},
                             new SelectListItem{Text="9:00", Value="9:00:00"},
                             new SelectListItem{Text="9:30", Value="9:30:00"},
                             new SelectListItem{Text="10:00",Value="10:00:00"},
                             new SelectListItem{Text="10:30",Value="10:30:00"},
                             new SelectListItem{Text="11:00",Value="11:00:00"},
                             new SelectListItem{Text="11:30",Value="11:30:00"},
                             new SelectListItem{Text="12:00",Value="12:00:00"},
                             new SelectListItem{Text="12:30",Value="12:30:00"},
                             new SelectListItem{Text="13:00",Value="13:00:00"},
                             new SelectListItem{Text="13:30",Value="13:30:00"},
                             new SelectListItem{Text="14:00",Value="14:00:00"},
                             new SelectListItem{Text="14:30",Value="14:30:00"},
                             new SelectListItem{Text="15:00",Value="15:00:00"},
                             new SelectListItem{Text="15:30",Value="15:30:00"},
                             new SelectListItem{Text="16:00",Value="16:00:00"},
                             new SelectListItem{Text="16:30",Value="16:30:00"},
                             new SelectListItem{Text="17:00",Value="17:00:00"},
                             new SelectListItem{Text="17:30",Value="17:30:00"},
                             new SelectListItem{Text="18:00",Value="18:00:00"},
                             new SelectListItem{Text="18:30",Value="18:30:00"}};
    }
}
