using HELPERLAND.Models.Data;
using HELPERLAND.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HELPERLAND.Models.ViewModels;

namespace HELPERLAND.Controllers
{
    [Authorize(Roles = "3")]
    public class AdminController : Controller
    {
        private readonly HelperlandContext helperlandContext;

        public AdminController(HelperlandContext _helperlandcontext)
        {
            helperlandContext = _helperlandcontext;
        }

        public IActionResult UserManagement()
        {
            return View();
        }


        [HttpPost]
        public JsonResult UserManagementData()
        {
            try
            {
                IFormCollection form = HttpContext.Request.ReadFormAsync().Result;
                var set = form.ToHashSet();
                var draw = form["draw"].FirstOrDefault();
                
                var start = form["start"].FirstOrDefault();
                var length = form["length"].FirstOrDefault();
                var sortColumn = form["columns[" + form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDir = form["order[0][dir]"].FirstOrDefault();
                var searchItems = form["search[value]"].FirstOrDefault().Length;

                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt16(start) : 0;
                int recordsTotal = 0;

                var data = helperlandContext.Users.Where(x => x.IsDeleted != true).ToList();


                if (searchItems > 0)
                {
                    var searchArray = form["search[value]"].FirstOrDefault().Split(",");
                    var userName = searchArray[0];
                    var userRole = searchArray[1];
                    var phoneNumber = searchArray[2];
                    var postalCode = searchArray[3];
                    var email = searchArray[4];
                    var fromDate = searchArray[5];
                    var toDate = searchArray[6];

                    if (!string.IsNullOrEmpty(userName))
                    {
                        data = data.Where(x => x.FirstName.Contains(userName, StringComparison.OrdinalIgnoreCase) || x.LastName.Contains(userName, StringComparison.OrdinalIgnoreCase)).ToList();
                    }

                    if (userRole != "userType" && !string.IsNullOrEmpty(userRole))
                    {
                        data = data.Where(x => x.UserTypeId == Int16.Parse(userRole)).ToList();

                    }

                    if (!string.IsNullOrEmpty(phoneNumber))
                    {
                        data = data.Where(x => x.Mobile.Contains(phoneNumber)).ToList();
                    }

                    if (!string.IsNullOrEmpty(postalCode))
                    {
                        data = data.Where(x => x.ZipCode == postalCode).ToList();
                    }

                    if (!string.IsNullOrEmpty(email))
                    {
                        data = data.Where(x => x.Email.Contains(email, StringComparison.OrdinalIgnoreCase)).ToList();
                    }

                    if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                    {
                        data = data.Where(x => x.CreatedDate >= DateTime.Parse(fromDate) && x.CreatedDate <= DateTime.Parse(toDate)).ToList();
                    }
                }

                if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDir))
                {
                    if (sortColumnDir.Equals("asc"))
                    {
                        switch (sortColumn)
                        {
                            case "User Name":
                                data = data.OrderBy(x => x.FirstName).ToList();
                                break;
                            case "Registration Date":
                                data = data.OrderBy(x => x.CreatedDate).ToList();
                                break;
                            case "Phone":
                                data = data.OrderBy(x => x.Mobile).ToList();
                                break;
                        }
                    }
                    else
                    {
                        switch (sortColumn)
                        {
                            case "User Name":
                                data = data.OrderByDescending(x => x.FirstName).ToList();
                                break;
                            case "Registration Date":
                                data = data.OrderByDescending(x => x.CreatedDate).ToList();
                                break;
                            case "Phone":
                                data = data.OrderByDescending(x => x.Mobile).ToList();
                                break;
                        }
                    }
                }
                recordsTotal = data.Count();
                data = data.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return Json("ok");
            }
        }


        public JsonResult Deactive(int id)
        {
            int adminId = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            User user = helperlandContext.Users.FirstOrDefault(x => x.UserId == id);
            user.IsActive = false;
            user.IsApproved = true;
            user.ModifiedDate = DateTime.Now;
            user.ModifiedBy = adminId;
            helperlandContext.Users.Update(user);
            helperlandContext.SaveChanges();
            return Json("ok");
        }


        public JsonResult Active(int id)
        {
            int adminId = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            User user = helperlandContext.Users.FirstOrDefault(x => x.UserId == id);
            user.IsActive = true;
            user.IsApproved = true;
            user.ModifiedDate = DateTime.Now;
            user.ModifiedBy = adminId;
            helperlandContext.Users.Update(user);
            helperlandContext.SaveChanges();
            return Json("ok");
        }


        public JsonResult Delete(int id)
        {
            int adminId = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
            User user = helperlandContext.Users.FirstOrDefault(x => x.UserId == id);
            user.IsDeleted = true;
            user.ModifiedDate = DateTime.Now;
            user.ModifiedBy = adminId;
            helperlandContext.Users.Update(user);
            helperlandContext.SaveChanges();
            return Json("ok");
        }


        public IActionResult ServiceRequest()
        {
            return View();
        }


        public IActionResult RequestDetail(int id)
        {
            ServiceRequest serviceRequest = helperlandContext.ServiceRequests.Include(x => x.User).Include(x => x.ServiceRequestAddresses).Include(x => x.ServiceRequestExtras).FirstOrDefault(x => x.ServiceRequestId == id);
            return PartialView(serviceRequest);
        }

        [HttpPost]
        public JsonResult ServiceRequestData()
        {
            try
            {
                IFormCollection form = HttpContext.Request.ReadFormAsync().Result;
                var set = form.ToHashSet();
                var draw = form["draw"].FirstOrDefault();
                
                var start = form["start"].FirstOrDefault(); ;
                var length = form["length"].FirstOrDefault();
                var sortColumn = form["columns[" + form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDir = form["order[0][dir]"].FirstOrDefault();

                var searchItems = form["search[value]"].FirstOrDefault().Length;
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt16(start) : 0;
                int recordsTotal = 0;

                var data = helperlandContext.ServiceRequests.Include(x => x.User).ThenInclude(x => x.UserAddresses.Where(x => x.IsDefault == true)).Include(x => x.ServiceProvider).ThenInclude(x => x.RatingRatingToNavigations).ToList();

                if (searchItems > 0)
                {
                    var searchArray = form["search[value]"].FirstOrDefault().Split(",");
                    var serviceId = searchArray[0];
                    var postalCode = searchArray[1];
                    var email = searchArray[2];
                    var customerName = searchArray[3];
                    var spName = searchArray[4];
                    var status = searchArray[5];
                    var hasIssue = searchArray[6];
                    var fromDate = searchArray[7];
                    var toDate = searchArray[8];

                    if (!string.IsNullOrEmpty(serviceId))
                    {
                        data = data.Where(x => x.ServiceRequestId == Int16.Parse(serviceId)).ToList();
                    }

                    if (!string.IsNullOrEmpty(postalCode))
                    {
                        data = data.Where(x => x.ZipCode == postalCode).ToList();
                    }

                    if (!string.IsNullOrEmpty(email))
                    {
                        data = data.Where(x => x.User.Email.Contains(email, StringComparison.OrdinalIgnoreCase) || x.ServiceProvider.Email.Contains(email, StringComparison.OrdinalIgnoreCase)).ToList();
                    }

                    if (!string.IsNullOrEmpty(customerName))
                    {
                        data = data.Where(x => x.User.FirstName.Contains(customerName, StringComparison.OrdinalIgnoreCase) || x.User.LastName.Contains(customerName, StringComparison.OrdinalIgnoreCase)).ToList();

                    }

                    if (!string.IsNullOrEmpty(spName))
                    {
                        data = data.Where(x => x.ServiceProvider?.FirstName == spName || x.ServiceProvider?.LastName == spName).ToList();

                    }
                    if (status != "selectStatus" && !string.IsNullOrEmpty(status))
                    {
                        data = data.Where(x => x.Status == Int16.Parse(status)).ToList();
                    }

                    if (bool.Parse(hasIssue))
                    {
                        data = data.Where(x => x.HasIssue == bool.Parse(hasIssue)).ToList();
                    }

                    if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                    {
                        if (Convert.ToDateTime(fromDate).CompareTo(Convert.ToDateTime(toDate)) < 0)
                        {
                            data = data.Where(x => x.ServiceStartDate >= DateTime.Parse(fromDate) && x.ServiceStartDate <= DateTime.Parse(toDate)).ToList();
                        }
                        else
                        {
                            data = data.Where(x => x.ServiceProviderId == 1).ToList();
                        }
                    }
                }


                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                {
                    if (sortColumnDir.Equals("asc"))
                    {
                        switch (sortColumn)
                        {
                            case "ServiceRequestId":
                                data = data.OrderBy(x => x.ServiceRequestId).ToList();
                                break;
                            case "ServiceStartDate":
                                data = data.OrderBy(x => x.ServiceStartDate).ToList();
                                break;
                            case "CustomerName":
                                data = data.OrderBy(x => x.User.FirstName).ToList();
                                break;
                            case "SPName":
                                data = data.OrderBy(x => x.ServiceProvider?.FirstName).ToList();
                                break;
                        }
                    }
                    else
                    {
                        switch (sortColumn)
                        {
                            case "ServiceRequestId":
                                data = data.OrderByDescending(x => x.ServiceRequestId).ToList();
                                break;
                            case "ServiceStartDate":
                                data = data.OrderByDescending(x => x.ServiceStartDate).ToList();
                                break;
                            case "CustomerName":
                                data = data.OrderByDescending(x => x.User.FirstName).ToList();
                                break;
                            case "SPName":
                                data = data.OrderByDescending(x => x.ServiceProvider?.FirstName).ToList();
                                break;
                        }
                    }
                }

                recordsTotal = data.Count();
                data = data.Skip(skip).Take(pageSize).ToList();

                List<AdminServiceRequest> adminServiceRequests = new List<AdminServiceRequest>();
                foreach (var request in data)
                {
                    var serviceRequest = new AdminServiceRequest();
                    serviceRequest.serviceRequestId = request.ServiceRequestId;
                    serviceRequest.serviceDate = request.ServiceStartDate.ToString("dd/MM/yyyy");
                    serviceRequest.serviceTime = request.ServiceStartDate.ToString("HH:mm") + "-" + request.ServiceStartDate.AddHours(request.ServiceHours).ToString("HH:mm");
                    serviceRequest.customerName = request.User.FirstName + " " + request.User.LastName;
                    serviceRequest.customerAddress = request.User.UserAddresses.ElementAt(0).AddressLine1 + " " + request.User.UserAddresses.ElementAt(0).AddressLine2 + "<br/>" + request.User.UserAddresses.ElementAt(0).PostalCode + " " + request.User.UserAddresses.ElementAt(0).City;
                    serviceRequest.spName = request.ServiceProvider?.FirstName + " " + request.ServiceProvider?.LastName;
                    serviceRequest.spAvtar = request.ServiceProvider?.UserProfilePicture;
                    serviceRequest.spRating = request.ServiceProvider?.RatingRatingToNavigations.Average(x => x.Ratings);
                    serviceRequest.totalCost = request.TotalCost.ToString();
                    serviceRequest.status = request.Status;
                    adminServiceRequests.Add(serviceRequest);
                }
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = adminServiceRequests });
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return Json("ok");
            }
        }




        [HttpGet]
        public IActionResult EditRequest(int id)
        {
            ServiceRequest? serviceRequest = helperlandContext.ServiceRequests.Include(x => x.ServiceRequestAddresses).FirstOrDefault(x => x.ServiceRequestId == id);
            AdminEditRequestViewModel adminEditRequestViewModel = new AdminEditRequestViewModel();
            adminEditRequestViewModel.ServiceRequestId = serviceRequest.ServiceRequestId;
            adminEditRequestViewModel.ServiceStartDate = serviceRequest.ServiceStartDate.Date;
            adminEditRequestViewModel.ServiceStartTime = serviceRequest.ServiceStartDate.ToString("hh:mm:ss");
            adminEditRequestViewModel.HouseNumber = serviceRequest.ServiceRequestAddresses.ElementAt(0).AddressLine1;
            adminEditRequestViewModel.StreetName = serviceRequest.ServiceRequestAddresses.ElementAt(0).AddressLine2;
            adminEditRequestViewModel.City = serviceRequest.ServiceRequestAddresses.ElementAt(0).City;
            adminEditRequestViewModel.PostalCode = serviceRequest.ServiceRequestAddresses.ElementAt(0).PostalCode;
            return PartialView(adminEditRequestViewModel);
        }


        [HttpPost]
        public IActionResult EditRequest(AdminEditRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                string newDateandTime = model.ServiceStartDate.ToString("dd/MM/yyyy") + " " + model.ServiceStartTime;
                DateTime newDate = DateTime.Parse(newDateandTime);
                ServiceRequest request = helperlandContext.ServiceRequests.Include(x => x.ServiceRequestAddresses).Include(x => x.User).Include(x => x.ServiceProvider).FirstOrDefault(x => x.ServiceRequestId == model.ServiceRequestId);
                request.ServiceStartDate = newDate;
                request.ServiceRequestAddresses.ElementAt(0).AddressLine1 = model.HouseNumber;
                request.ServiceRequestAddresses.ElementAt(0).AddressLine2 = model.StreetName;
                request.ServiceRequestAddresses.ElementAt(0).PostalCode = model.PostalCode;
                request.ServiceRequestAddresses.ElementAt(0).City = model.City;
                if (model.RescheduleReason != null)
                {
                    request.Comments = model.RescheduleReason;
                }
                request.ModifiedBy = Int16.Parse(User.Claims.FirstOrDefault(x => x.Type == "userId").Value);
                request.ModifiedDate = DateTime.Now;
                helperlandContext.ServiceRequests.Update(request);
                helperlandContext.SaveChanges();


                List<string> emailList = new List<string>();
                emailList.Add(request.User.Email);
                if (request.ServiceProvider != null)
                {
                    emailList.Add(request.ServiceProvider.Email);
                }
                string subject = "About the request..";
                string body = "Admin has reschedule the request and the request id is " + request.ServiceRequestId + "and the new date is " + newDate;
                EmailManager.SendEmail(emailList, subject, body);
                var msg = "Time Rescheduled..";
                ViewBag.Alert= "<div class='alert alert-success alert-dismissible fade show' role='alert'>" + msg + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                return PartialView(model);

            }
            else
            {
                return PartialView(model);
            }
        }




        [HttpGet]
        public IActionResult CancelRequest()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult CancelRequest(int id, string comment)
        {
            
            ServiceRequest serviceRequest = helperlandContext.ServiceRequests.Include(x => x.User).Include(x => x.ServiceProvider).FirstOrDefault(x => x.ServiceRequestId == id);
            
            serviceRequest.Comments = comment;
            
            serviceRequest.Status = 2;
            serviceRequest.ModifiedDate = DateTime.Now;
            helperlandContext.ServiceRequests.Update(serviceRequest);
            helperlandContext.SaveChanges();
            List<string> emailList = new List<string>();
            emailList.Add(serviceRequest.User.Email);
            
            if (serviceRequest.ServiceProvider != null)
            {
                emailList.Add(serviceRequest.ServiceProvider.Email);
            }
            string subject = "Request Cancelled By Admin!";
            string body = "Service Request " + serviceRequest.ServiceRequestId + " has been cancelled by Admin.";
            EmailManager.SendEmail(emailList, subject, body);
            
            var message = "Request cancelled successfully..";
            ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
            return PartialView();
        }
    }
}
