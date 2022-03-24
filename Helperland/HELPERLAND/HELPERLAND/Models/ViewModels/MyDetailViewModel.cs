﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HELPERLAND.Models.ViewModels
{
    public class MyDetailViewModel
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "E-mail address")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Mobile number")]
        public string Mobile { get; set; }

        public string BirthDay { get; set; }
        public string BirthMonth { get; set; }
        public string BirthYear { get; set; }

        [Display(Name = "My Preffered Language")]
        public int? LanguageId { get; set; }
    }
}
