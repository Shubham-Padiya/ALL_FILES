using HELPERLAND.Models;
using HELPERLAND.Models.Data;
using HELPERLAND.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HELPERLAND.Controllers
{
    [Authorize]
    public class Bookservicecontroller : Controller
    {
        private readonly HelperlandContext helperlandContext;
        public Bookservicecontroller(HelperlandContext _helperlandContext)
        {
            helperlandContext = _helperlandContext;
        }
        public IActionResult Bookservice()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ZipCode(ZipCodeViewModel model) 
        {
            if (ModelState.IsValid)
            {
                var isServiceAvailable = helperlandContext.Users.Any(user => user.ZipCode == model.ZipCode && user.UserTypeId == 2);
                if (isServiceAvailable)
                {
                    HttpContext.Session.SetString("ZipCodeViewModel", JsonConvert.SerializeObject(model));
                    ViewBag.Message = "Schedual";
                    return PartialView(model);
                }
                else
                {
                    var message = "we are not providing any service in this area.";
                    ViewBag.Alert = "<div class='alert alert-danger alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    return View(model);
                }
            }
            else
            {
                return PartialView(model);
            }
        }

        [HttpPost]
        public IActionResult Schedual(SchedualViewModel model)
        {
            if (ModelState.IsValid)
            {
                var day = model.ServiceDate.ToString("dd-MM-yyyy");
                var time = model.ServiceTime.ToString("hh:mm:ss");
                var dateandtime = day + " " + time;
                DateTime dt = DateTime.Parse(dateandtime);
                model.ServiceStartDate = dt;
                HttpContext.Session.SetString("SchedualViewModel", JsonConvert.SerializeObject(model));
                ViewBag.Message = "urdetail";
                return PartialView(model);
            }
            else
            {
                return Json(ModelState.ValidationState);
            }
        }

        [HttpGet]
        public IActionResult YourDetail()
        {
            DetailViewModel model = new DetailViewModel();
            model.userAddress = helperlandContext.UserAddresses.Where(address => address.UserId == 1 && address.PostalCode == "360003").ToList();
            HttpContext.Session.SetString("useraddresslist", JsonConvert.SerializeObject(model.userAddress));
            return PartialView(model);
        }

        [HttpPost]
        public IActionResult YourDetail(DetailViewModel model)
        {
            var value = HttpContext.Session.GetString("useraddresslist");
            model.userAddress = JsonConvert.DeserializeObject<IEnumerable<UserAddress>>(value);
            HttpContext.Session.SetString("DetailViewModel", JsonConvert.SerializeObject(model));
            ViewBag.Message = "payment";
            return PartialView(model);
        }


        [HttpPost]
        public IActionResult MakePayment([FromBody] ServiceRequest serviceRequest)
        {
            var schedualviewmodelstring = HttpContext.Session.GetString("SchedualViewModel");
            SchedualViewModel schedualViewModel = JsonConvert.DeserializeObject<SchedualViewModel>(schedualviewmodelstring);
            var detailviewmodelstring = HttpContext.Session.GetString("DetailViewModel");
            DetailViewModel detailViewModel = JsonConvert.DeserializeObject<DetailViewModel>(detailviewmodelstring);
            var zipcodeviewmodelstring = HttpContext.Session.GetString("ZipCodeViewModel");
            ZipCodeViewModel zipCodeViewModel = JsonConvert.DeserializeObject<ZipCodeViewModel>(zipcodeviewmodelstring);

            UserAddress userAddress = detailViewModel.userAddress.FirstOrDefault(address => address.AddressId == detailViewModel.check);

            serviceRequest.UserId = 1;
            serviceRequest.ZipCode = zipCodeViewModel.ZipCode;
            serviceRequest.ServiceStartDate = schedualViewModel.ServiceStartDate;
            serviceRequest.ServiceHours = schedualViewModel.ServiceHours;
            serviceRequest.Comments = schedualViewModel.Comments;
            serviceRequest.HasPets = schedualViewModel.HasPets;
            serviceRequest.CreatedDate = DateTime.Now;
            serviceRequest.ModifiedDate = DateTime.Now;
            serviceRequest.ModifiedBy = 1;

            serviceRequest.ServiceRequestAddresses.Add(new ServiceRequestAddress()
            {
                AddressLine1 = userAddress.AddressLine1,
                AddressLine2 = userAddress.AddressLine2,
                City = userAddress.City,
                PostalCode = userAddress.PostalCode,
                Email = userAddress.Email,
                Mobile = userAddress.Mobile
            });
            helperlandContext.ServiceRequests.Add(serviceRequest);
            helperlandContext.SaveChanges();
            ViewBag.IsError = false;
            ViewBag.ResultMessage = "Booking has been done successfully..";
            ViewBag.ServiceRequestId = serviceRequest.ServiceRequestId;

            var emailList = helperlandContext.Users.Where(user => user.UserTypeId == 2 && user.ZipCode == zipCodeViewModel.ZipCode).Select(user => user.Email).ToList();
            string subject = "New Service Request";
            string body = "There is new Service Request in your are and Request Id is : " + serviceRequest.ServiceRequestId;
            EmailManager.SendEmail(emailList, subject, body);
            return PartialView();
        }
    }
}
