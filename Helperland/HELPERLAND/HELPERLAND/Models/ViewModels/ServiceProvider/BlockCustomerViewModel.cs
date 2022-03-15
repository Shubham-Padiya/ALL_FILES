using HELPERLAND.Models.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HELPERLAND.Models.ViewModels.ServiceProvider
{
    public class BlockCustomerViewModel
    {
        public IEnumerable<User> allCustomer { get; set; }
        public IEnumerable<User> blockedCustomer { get; set; }
    }
}
