using HELPERLAND.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using HELPERLAND.Models.ViewModels;
using System.Threading.Tasks;
using HELPERLAND.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace HELPERLAND.Controllers
{
    [Authorize(Roles ="1")]
    public class CustomerController : Controller
    {
        private readonly HelperlandContext helperlandContext;
        public CustomerController(HelperlandContext _helperlandContext)
        {
            helperlandContext = _helperlandContext;
        }

        public IActionResult ServiceHistory()
        {
            // status 1=completed , status 2=cancled , status 3=refunded , status 4=pending
            IEnumerable<ServiceRequest> serviceRequests = helperlandContext.ServiceRequests.Include(x => x.ServiceProvider).Where(x => x.Status!=4).ToList();
            return View(serviceRequests);
        }

        public IActionResult ServiceRequest()
        {
            IEnumerable<ServiceRequest> serviceRequests = helperlandContext.ServiceRequests.Include(x => x.ServiceProvider).Where(x => x.Status == 4).ToList();
            return View(serviceRequests);
        }

        [HttpGet]
        public IActionResult ServiceDetail(int id)
        {
            ServiceRequest serviceRequest = helperlandContext.ServiceRequests.Include(x=>x.ServiceRequestAddresses).Include(x=>x.ServiceRequestExtras).FirstOrDefault(x => x.ServiceRequestId == id);
            return PartialView(serviceRequest);
        }

        [HttpGet]
        public IActionResult Reschedule(int id)
        {
            RescheduleViewModel model = new RescheduleViewModel();
            model.ServiceRequestId = id;
            return PartialView(model);
        }

        [HttpPost]
        public IActionResult Reschedule(RescheduleViewModel model)
        {
            var isIntruption = false;
            ServiceRequest serviceRequest = helperlandContext.ServiceRequests.Include(x => x.ServiceProvider).FirstOrDefault(x => x.ServiceRequestId == model.ServiceRequestId);
            var day = model.ServiceDate.ToString("dd/MM/yyyy");
            var time = model.ServiceTime.ToString("hh:mm");
            var dateandtime = day + " " + time;
            DateTime newStartDate = DateTime.Parse(dateandtime);

            //if request has not yet accepted..
            if (serviceRequest.ServiceProvider == null)
            {
                serviceRequest.ServiceStartDate = newStartDate;
                serviceRequest.ModifiedDate = DateTime.Now;
                var msg = "Your service time has been rescheduled..";
                ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>" + msg + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                helperlandContext.ServiceRequests.Update(serviceRequest);
                helperlandContext.SaveChanges();
                return PartialView(model);
            }
            else
            {
                var newStartTime = newStartDate;
                var newEndTime = newStartTime.AddHours(serviceRequest.ServiceHours);

                //all that service which are at same SP and 
                IEnumerable<ServiceRequest> serviceRequests = helperlandContext.ServiceRequests.Where(x => x.ServiceProviderId == serviceRequest.ServiceProviderId && x.Status == 4 && x.ServiceRequestId != serviceRequest.ServiceRequestId).ToList();
                foreach(var requests in serviceRequests)
                {
                    var oldStartTime = requests.ServiceStartDate;
                    var oldEndTime = oldStartTime.AddHours(requests.ServiceHours);
                    isIntruption = false;

                    if((requests.ServiceStartDate == newStartDate) || (((newStartTime > oldStartTime) && (newStartTime < oldEndTime)) || ((newEndTime > oldStartTime) && (newEndTime<oldEndTime))))
                    {
                        isIntruption = true;
                        break;
                    }
                }
                if (isIntruption)
                {
                    var msg = "Another service request has been assigned to the service provider on " + newStartDate.Date.ToString() + " from " + newStartTime.TimeOfDay.ToString() + " to" + newEndTime.TimeOfDay.ToString() + ". Either choose another date or pick up a different time slot.";
                    ViewBag.Alert = "<div class='alert alert-danger alert-dismissible fade show' role='alert'>" + msg + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    return PartialView(model);
                }
                else
                {
                    serviceRequest.ServiceStartDate = newStartDate;
                    serviceRequest.ModifiedDate = DateTime.Now;
                    helperlandContext.ServiceRequests.Update(serviceRequest);
                    helperlandContext.SaveChanges();

                    List<string> emailList = new List<string>();
                    emailList.Add(serviceRequest.ServiceProvider.Email);

                    string subject = "Request Rescheduled!";
                    string body = "Service Request " + serviceRequest.ServiceRequestId + " has been rescheduled by customer. New date and time are " + newStartDate.ToString();
                    EmailManager.SendEmail(emailList, subject, body);

                    var msg = "Your service time has been rescheduled..";
                    ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>" + msg + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    return PartialView(model);
                }
            }
        }

        [HttpGet]
        public IActionResult Cancel()
        {
            return PartialView();
        }


        [HttpPost]
        public IActionResult CancelServiceRequest(int id, string comment)
        {
            ServiceRequest serviceRequest = helperlandContext.ServiceRequests.Include(x => x.ServiceProvider).FirstOrDefault(x => x.ServiceRequestId == id);
            
            serviceRequest.Comments = comment;
            
            serviceRequest.Status = 2;
            serviceRequest.ModifiedDate = DateTime.Now;
            helperlandContext.ServiceRequests.Update(serviceRequest);
            helperlandContext.SaveChanges();
            
            if (serviceRequest.ServiceProvider != null)
            {
                List<string> emailList = new List<string>();
                emailList.Add(serviceRequest.ServiceProvider.Email);
                
                string subject = "Request Cancelled!";
                string body = "Service Request " + serviceRequest.ServiceRequestId + " has been cancelled by customer.";
                EmailManager.SendEmail(emailList, subject, body);
            }
            
            var message = "Request cancelled successfully..";
            ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
            return PartialView();
        }

        public IActionResult MySetting()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MyDetail()
        {

            string currentUser = HttpContext.Session.GetString("CurrentUser");
            User user = JsonConvert.DeserializeObject<User>(currentUser);
            MyDetailViewModel model = new MyDetailViewModel();
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.Mobile = user.Mobile;
            model.LanguageId = user.LanguageId;
            if (user.DateOfBirth != null)
            {
                var DOB = user.DateOfBirth.Value.ToString("dd/MMMM/yyyy").Split("-");
                model.BirthDay = DOB[0];
                model.BirthMonth = DOB[1];
                model.BirthYear = DOB[2];
            }

            return PartialView(model);
        }

        [HttpPost]
        public IActionResult MyDetail(MyDetailViewModel model)
        {

            string currentUser = HttpContext.Session.GetString("CurrentUser");
            User user = JsonConvert.DeserializeObject<User>(currentUser);
            string DOB = model.BirthDay + "-" + model.BirthMonth + "-" + model.BirthYear;
            user.DateOfBirth = DateTime.Parse(DOB);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Mobile = model.Mobile;
            user.LanguageId = model.LanguageId;

            helperlandContext.Users.Update(user);
            helperlandContext.SaveChanges();

            HttpContext.Session.SetString("CurrentUser", JsonConvert.SerializeObject(user));
            var message = "Details Updated successfully..";
            ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
            return PartialView(model);
        }

        [HttpGet]
        public IActionResult MyAddresses()
        {
            int id = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            IEnumerable<UserAddress> userAddresses = helperlandContext.UserAddresses.Where(x => x.UserId == id).ToList();
            return PartialView(userAddresses);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePassViewModel model)
        {
            if (ModelState.IsValid)
            {

                string currentUser = HttpContext.Session.GetString("CurrentUser");
                User user = JsonConvert.DeserializeObject<User>(currentUser);
                
                if (user.Password.Equals(model.CurrentPassword))
                {
                    user.Password = model.ConfirmPassword;
                    helperlandContext.Users.Update(user);
                    helperlandContext.SaveChanges();
                    HttpContext.Session.SetString("CurrentUser", JsonConvert.SerializeObject(user));
                    model = null;
                    var message = "Password changed successfully..";
                    ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    return PartialView(model);
                }
                else
                {
                    var message = "Incorrect old password";
                    ViewBag.Alert = "<div class='alert alert-danger alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    return PartialView(model);
                }
            }
            else
            {
                return PartialView(model);
            }
        }
    }
}
