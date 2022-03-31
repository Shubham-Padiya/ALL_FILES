using HELPERLAND.Models.Data;
using HELPERLAND.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using HELPERLAND.Models;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace HELPERLAND.Controllers
{
    public class ForUserController : Controller
    {
        private readonly HelperlandContext helperlandContext;

        public ForUserController(HelperlandContext _helperlandcontext)
        {
            helperlandContext = _helperlandcontext;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            string message;
            if (ModelState.IsValid)
            {
                User user = helperlandContext.Users.Where(user => user.Email.Equals(model.Email) && user.Password.Equals(model.Password)).FirstOrDefault();
                if(user != null)
                {
                    if(user.IsApproved && user.IsActive)
                    {
                        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

                        identity.AddClaim(new Claim("userId", user.UserId.ToString()));
                        identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
                        identity.AddClaim(new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName));
                        identity.AddClaim(new Claim(ClaimTypes.Role, user.UserTypeId.ToString()));

                        var principal = new ClaimsPrincipal(identity);

                        var authProperties = new AuthenticationProperties
                        {
                            AllowRefresh = true,
                            ExpiresUtc = DateTimeOffset.Now.AddMinutes(30),
                            IsPersistent = model.RememberMe,
                        };

                        HttpContext.Session.SetString("CurrentUser", JsonConvert.SerializeObject(user));


                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
                        string returnUrl = (string)TempData["returnUrl"];
                        if (returnUrl != null)
                        {
                            return Json("returnUrl=" + returnUrl);
                        }
                        else
                        {
                            switch (user.UserTypeId)
                            {
                                case 1: return Json("returnUrl=/customer/servicerequest");
                                case 2: return Json("returnUrl=/serviceprovider/newservicerequest");
                                case 3: return Json("returnUrl=/admin/servicerequest");
                                default: return Json("returnUrl=/home/index");
                            }
                        }
                    }
                    else
                    {
                        message = "Your account is not active or not approved by admin ..Please contact to admin..";
                        ViewBag.Alert = "<div class='alert alert-danger alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                        return View(model);
                    }
                }
                else
                {
                    message = "Invalid username or password";
                    ViewBag.Alert = "<div class='alert alert-danger alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }

        }



        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return PartialView();
        }

        [HttpPost]
        public IActionResult ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isEmailExists = helperlandContext.Users.Any(user => user.Email == model.Email);
                if (isEmailExists)
                {
                    string Token = Guid.NewGuid().ToString();
                    var lnkHref = "<a href='" + Url.Action("ResetPass", "ForUser", new { email = model.Email, code = Token }, "http") + "'>Reset Password</a>";

                    string subject = "Reset Password Link";
                    string body = "<b>Please find the Password Reset Link. </b><br/>" + lnkHref;
                    List<string> toList = new List<string>();
                    toList.Add(model.Email);
                    EmailManager.SendEmail(toList, subject, body);
                    ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>we have sent Password reset link on your Email..<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    return View();
                }
                else
                {
                    ViewBag.Alert = "<div class='alert alert-danger alert-dismissible fade show' role='alert'>Email address not found..make sure that you have entered a correct one<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    return View();
                }
            }
            else 
            {
                return View(model);
            }
        }




        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var isEmailAlreadyExists = helperlandContext.Users.Any(user => user.Email == model.Email);
                if(isEmailAlreadyExists)
                {
                    ViewBag.Alert = "<div class='alert alert-danger alert-dismissible fade show' role='alert'>User with this email already exists<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    return View(model);
                }
                else
                {
                    User user = new User()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Mobile = model.Mobile,
                        Password = model.Password,
                        UserTypeId = 1,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IsApproved = true,
                        IsActive = true,
                    };
                    helperlandContext.Users.Add(user);
                    helperlandContext.SaveChanges();
                    ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>You have successfully registered..<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                    return View();
                }
            }
            else
            {
                return View(model);
            }
        }





        [HttpGet]
        public IActionResult ResetPass(string Email)
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            model.Email = Email;
            return View(model);
        }

        [HttpPost]
        public IActionResult ResetPass(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = helperlandContext.Users.Where(user => user.Email.Equals(model.Email)).FirstOrDefault();
                user.Password = model.Password;
                user.ModifiedDate = DateTime.Now;
                user.ModifiedBy = user.UserId;
                helperlandContext.Users.Update(user);
                helperlandContext.SaveChanges();
                ViewBag.Alert = "<div class='alert alert-success alert-dismissible fade show' role='alert'>Your Password has been changed...<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                return View();
            }
            else
            {
                return View(model);
            }
        }


        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("index", "home");
        }
    }
}
