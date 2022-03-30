using HELPERLAND.Models;
using HELPERLAND.Models.Data;
using HELPERLAND.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using HELPERLAND.Models.ViewModels.ServiceProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HELPERLAND.Controllers
{
    [Authorize (Roles ="2")]
    public class ServiceProviderController : Controller
    {
        private readonly HelperlandContext helperlandContext;
        public ServiceProviderController(HelperlandContext _helperlandContext)
        {
            helperlandContext = _helperlandContext;
        }


        [HttpGet]
        public IActionResult ServiceDetails(int id)
        {
            ServiceRequest serviceRequest = helperlandContext.ServiceRequests.Include(x => x.User).Include(x=>x.ServiceRequestAddresses).Include(x => x.ServiceRequestExtras).FirstOrDefault(x => x.ServiceRequestId == id);
            return PartialView(serviceRequest);
        }


        public IActionResult NewServiceRequest()
        {
            string currentUser = HttpContext.Session.GetString("CurrentUser");
            User user = JsonConvert.DeserializeObject<User>(currentUser);


            int spId = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            IEnumerable<int> blockedCustomerList = helperlandContext.FavoriteAndBlockeds.Where(x => x.UserId == spId && x.IsBlocked == true).Select(x => x.TargetUserId).Distinct().ToList();
            //someone has booked service but not accepted yet except blocked customer...
            IEnumerable<ServiceRequest> serviceRequests = helperlandContext.ServiceRequests.Include(x => x.User).Include(x => x.ServiceRequestAddresses).Where(x => x.Status == 5 && x.ZipCode==user.ZipCode && !blockedCustomerList.Any(block => block == x.UserId)).ToList();
            return View(serviceRequests);
        }

        [HttpGet]
        public IActionResult AcceptService()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult AcceptService(int id)
        {

            int spId = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            int conflictedServiceId = 0;
            bool isConflict = false;

            ServiceRequest serviceRequest = helperlandContext.ServiceRequests.Include(x => x.User).Include(x => x.ServiceRequestAddresses).FirstOrDefault(x => x.ServiceRequestId == id);
            HttpContext.Session.SetString("CurrentServiceRecordVersion", serviceRequest.RecordVersion.Value.ToString());

            string recordVersion = HttpContext.Session.GetString("CurrentServiceRecordVersion");
            if (recordVersion == serviceRequest.RecordVersion.Value.ToString())
            {
                IEnumerable<ServiceRequest> upcomingRequest = helperlandContext.ServiceRequests.Where(x => x.ServiceProviderId == spId && x.Status == 4).ToList();
                if (upcomingRequest.Count() > 0)
                {
                    foreach(var request in upcomingRequest)
                    {
                        if (request.ServiceStartDate.Date.Equals(serviceRequest.ServiceStartDate.Date))
                        {
                            TimeSpan oldRequestStartTime = request.ServiceStartDate.TimeOfDay;
                            TimeSpan oldRequestEndTime = request.ServiceStartDate.AddHours(request.ServiceHours).TimeOfDay;
                            TimeSpan currentRequestStartTime = request.ServiceStartDate.TimeOfDay;
                            TimeSpan currentRequestEndTime = request.ServiceStartDate.AddHours(request.ServiceHours).TimeOfDay;

                            if (currentRequestStartTime > oldRequestEndTime)
                            {
                                double timeDiffrence = currentRequestStartTime.Subtract(oldRequestEndTime).TotalHours;
                                if(timeDiffrence < 1)
                                {
                                    isConflict = true;
                                    conflictedServiceId = request.ServiceRequestId;
                                    break;
                                }
                            }
                            if (currentRequestEndTime < oldRequestStartTime)
                            {
                                double timeDiffrence1 = oldRequestStartTime.Subtract(currentRequestEndTime).TotalHours;
                                if (timeDiffrence1 < 1)
                                {
                                    isConflict = true;
                                    conflictedServiceId = request.ServiceRequestId;
                                    break;
                                }
                            }
                            if (currentRequestStartTime == oldRequestStartTime)
                            {
                                isConflict = true;
                                conflictedServiceId = request.ServiceRequestId;
                                break;
                            }
                            if (((currentRequestStartTime > oldRequestStartTime) && (currentRequestStartTime < oldRequestEndTime)) || ((currentRequestEndTime > oldRequestStartTime) && (currentRequestEndTime < oldRequestEndTime)))
                            {
                                isConflict = true;
                                conflictedServiceId = request.ServiceRequestId;
                                break;
                            }
                        }
                    }
                }
                if (isConflict)
                {
                    var message = "Another service request " + conflictedServiceId + " has already been assigned which has time conflicted with this service request. You can’t pick this Request now !";
                    ViewBag.Alert = "<div class='alert alert-danger alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    return PartialView(serviceRequest);
                }
                else
                {
                    serviceRequest.ServiceProviderId = spId;
                    serviceRequest.Status = 4;
                    serviceRequest.ModifiedBy = spId;
                    serviceRequest.ModifiedDate = DateTime.Now;
                    serviceRequest.RecordVersion = Guid.NewGuid();
                    helperlandContext.ServiceRequests.Update(serviceRequest);
                    helperlandContext.SaveChanges();
                    var emailList = helperlandContext.Users.Where(user => user.UserTypeId == 2 && user.ZipCode == serviceRequest.ServiceRequestAddresses.ElementAt(0).PostalCode && user.UserId != spId).Select(user => user.Email).ToList();
                    string subject = "Service request no more available";
                    string body = "Service Request " + serviceRequest.ServiceRequestId + " has already been accepted by someone and is no more available to you.";
                    EmailManager.SendEmail(emailList, subject, body);
                    var message = "Request assigned to you";
                    ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    return PartialView(serviceRequest);
                }
            }
            else
            {
                var message = "This service request is no more available. It has been assigned to another provider.";
                ViewBag.Alert = "<div class='alert alert-danger alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                return PartialView(serviceRequest);
            }
        }

        public IActionResult UpcomingServiceRequest()
        {
            int spId = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            //upcoming service request contains that request which are accepted by provider but not completed so its status is 4 which is  pending
            IEnumerable<ServiceRequest> serviceRequests = helperlandContext.ServiceRequests.Include(x => x.User).Include(x => x.ServiceRequestAddresses).Where(x => x.Status == 4 && x.ServiceProviderId == spId).ToList();
            return View(serviceRequests);
        }


        [HttpGet]
        public IActionResult CancelRequest()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult CancelRequest(int id , string comment)
        {
            int spId = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            ServiceRequest serviceRequest = helperlandContext.ServiceRequests.Include(x => x.User).FirstOrDefault(x => x.ServiceRequestId == id);
            serviceRequest.Comments = comment;
            //we have setted request as cancelled.
            serviceRequest.Status = 2;
            serviceRequest.ModifiedDate = DateTime.Now;
            serviceRequest.ModifiedBy = spId;
            helperlandContext.ServiceRequests.Update(serviceRequest);
            helperlandContext.SaveChanges();

            //now we have to inform user that service has been cancelled...
            List<string> emailList = new List<string>();
            emailList.Add(serviceRequest.User.Email);
            string subject = "Your Request has been cancelled...";
            string body = "Your Service request is cancelled by your service provider who is" + User.Identity.Name.Split("").ElementAt(0) + ".";
            EmailManager.SendEmail(emailList, subject, body);

            //show message to provider..
            var msg = "Request has been cancelled..";
            ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>" + msg + "<button class='btn-close' data-bs-dismiss='alert' aria-label=''Close></button></div>";
            return PartialView();
        }

        
        [HttpGet]
        public IActionResult CompleteService()
        {
            return PartialView();
        }
        [HttpPost]
        public IActionResult CompleteService(int id)
        {
            int spId = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            ServiceRequest serviceRequest = helperlandContext.ServiceRequests.Include(x => x.User).FirstOrDefault(x => x.ServiceRequestId == id);
            serviceRequest.Status = 1;
            serviceRequest.ModifiedDate = DateTime.Now;
            serviceRequest.ModifiedBy = spId;
            helperlandContext.ServiceRequests.Update(serviceRequest);
            helperlandContext.SaveChanges();

            Rating rating = new Rating();
            rating.ServiceRequestId = serviceRequest.ServiceRequestId;
            rating.RatingTo = spId;
            rating.RatingFrom = serviceRequest.UserId;
            rating.RatingDate = DateTime.Now;
            rating.OnTimeArrival = 0;
            rating.QualityOfService = 0;
            rating.Friendly = 0;
            rating.Ratings = 0;
            helperlandContext.Ratings.Add(rating);
            helperlandContext.SaveChanges();
            
            var msg = "Request completed..";
            ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>" + msg + "<button class='btn-close' data-bs-dismiss='alert' aria-label=''Close></button></div>";
            return PartialView();
        }


        public IActionResult ServiceHistory()
        {
            int id = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            IEnumerable<ServiceRequest> serviceRequests = helperlandContext.ServiceRequests.Include(x => x.User).Include(x => x.ServiceRequestAddresses).Where(x => x.ServiceProviderId == id && x.Status == 1).ToList();
            return View(serviceRequests);
        }

        public IActionResult MySetting()
        {
            return View();
        }


        [HttpGet]
        public IActionResult MyDetails()
        {
            int id = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            User user = helperlandContext.Users.Include(x => x.UserAddresses).FirstOrDefault(x => x.UserId == id);
            MyDetailsViewModel model = new MyDetailsViewModel();
            model.IsActive = user.IsActive;
            model.UserProfilePicture = user.UserProfilePicture;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.Mobile = user.Mobile;
            model.Nationality = user.NationalityId;
            model.Gender = user.Gender;
            if (user.DateOfBirth != null)
            {
                var DOB = user.DateOfBirth.Value.ToString("dd/MMMM/yyyy").Split("-");
                model.BirthDay = DOB[0];
                model.BirthMonth = DOB[1];
                model.BirthYear = DOB[2];
            }
            if (user.UserAddresses.Count == 1)
            {
                model.AddressId = user.UserAddresses.ElementAt(0).AddressId;
                model.StreetName = user.UserAddresses.ElementAt(0).AddressLine1;
                model.HouseNumber = user.UserAddresses.ElementAt(0).AddressLine2;
                model.City = user.UserAddresses.ElementAt(0).City;
                model.PostalCode = user.ZipCode;
            }
            return PartialView(model);
        }

        [HttpPost]
        public IActionResult MyDetails(MyDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                int id = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
                User user = helperlandContext.Users.Include(x => x.UserAddresses.Where(x => x.IsDefault == true)).FirstOrDefault(x => x.UserId == id);
                string DOB = model.BirthDay + "-" + model.BirthMonth + "-" + model.BirthYear;
                user.DateOfBirth = DateTime.Parse(DOB);
                user.UserProfilePicture = model.UserProfilePicture;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Mobile = model.Mobile;
                user.NationalityId = model.Nationality;
                user.Gender = model.Gender;
                if (user.UserAddresses.Count == 1)
                {
                    user.UserAddresses.ElementAt(0).AddressLine1 = model.StreetName;
                    user.UserAddresses.ElementAt(0).AddressLine2 = model.HouseNumber;
                    user.UserAddresses.ElementAt(0).City = model.City;
                    user.UserAddresses.ElementAt(0).PostalCode = model.PostalCode;
                    user.ZipCode = model.PostalCode;
                    helperlandContext.Users.Update(user);
                    helperlandContext.SaveChanges();
                    user.UserAddresses.Clear();
                }
                
                else
                {
                    UserAddress userAddress = new UserAddress();
                    userAddress.UserId = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
                    userAddress.AddressLine1 = model.StreetName;
                    userAddress.AddressLine2 = model.HouseNumber;
                    userAddress.City = model.City;
                    userAddress.PostalCode = model.PostalCode;
                    user.ZipCode = model.PostalCode;
                    helperlandContext.UserAddresses.Update(userAddress);
                    helperlandContext.Users.Update(user);
                    helperlandContext.SaveChanges();
                    user.UserAddresses.Clear();
                }
                

                HttpContext.Session.SetString("CurrentUser", JsonConvert.SerializeObject(user));
                var message = "Details Updated successfully..";
                ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                return PartialView(model);
            }
            else
            {
                return PartialView(model);
            }
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
                    ModelState.Clear();
                    HttpContext.Session.SetString("CurrentUser", JsonConvert.SerializeObject(user));
                    var msg = "Password changed successfully..";
                    ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>" + msg + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    return PartialView(model);
                }
                else
                {
                    var msg = "Incorrect old password";
                    ViewBag.Alert = "<div class='alert alert-danger alert-dismissible fade show' role='alert'>" + msg + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    return PartialView(model);
                }
            }
            else
            {
                return PartialView(model);
            }
        }


        public IActionResult BlockCustomer()
        {
            int spID = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            BlockCustomerViewModel model = new BlockCustomerViewModel();
            
            //fetch list of all customers for whom service provider had worked
            model.allCustomer = helperlandContext.ServiceRequests.Include(x => x.User).Where(x => x.Status == 1 && x.ServiceProviderId == spID).Select(x => x.User).Distinct().AsEnumerable();
            
            //fetch list of blocked customer
            model.blockedCustomer = helperlandContext.FavoriteAndBlockeds.Include(x => x.TargetUser).Where(x => x.UserId == spID && x.IsBlocked == true).Select(x => x.TargetUser).ToList();
           
            //return view with service list
            return View(model);
        }


        [HttpPost]
        public JsonResult blockcustomer(int customerId)
        {
            int spID = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            FavoriteAndBlocked favoriteAndBlocked = helperlandContext.FavoriteAndBlockeds.FirstOrDefault(x => x.UserId == spID && x.TargetUserId == customerId);
            if (favoriteAndBlocked == null)
            {
                favoriteAndBlocked = new FavoriteAndBlocked();
                favoriteAndBlocked.TargetUserId = customerId;
                favoriteAndBlocked.UserId = spID;
                favoriteAndBlocked.IsBlocked = true;
                favoriteAndBlocked.IsFavorite = false;
                helperlandContext.FavoriteAndBlockeds.Add(favoriteAndBlocked);
                helperlandContext.SaveChanges();
                return Json("ok");
            }
            else
            {
                favoriteAndBlocked.IsBlocked = true;
                favoriteAndBlocked.IsFavorite = false;
                helperlandContext.FavoriteAndBlockeds.Update(favoriteAndBlocked);
                helperlandContext.SaveChanges();
                return Json("ok");
            }
        }

        [HttpPost]
        public JsonResult unblockCustomer(int customerId)
        {
            //unblock particular customer
            int spID = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            FavoriteAndBlocked favoriteAndBlocked = helperlandContext.FavoriteAndBlockeds.FirstOrDefault(x => x.UserId == spID && x.TargetUserId == customerId);
            favoriteAndBlocked.IsBlocked = false;
            favoriteAndBlocked.IsFavorite = true;
            helperlandContext.FavoriteAndBlockeds.Update(favoriteAndBlocked);
            helperlandContext.SaveChanges();
            return Json("ok");
        }


        public IActionResult MyRatings()
        {
            int spID = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            IEnumerable<Rating> ratings = helperlandContext.Ratings.Include(x => x.ServiceRequest).Include(x => x.RatingFromNavigation).Where(x => x.RatingTo == spID);
            return View(ratings);
        }
    }
}
