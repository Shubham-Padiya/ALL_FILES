using HELPERLAND.Models.Data;
using HELPERLAND.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using HELPERLAND.Models;
using System.Threading.Tasks;

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
        public IActionResult Login(LoginViewModel model)
        {
            string message;
            if (ModelState.IsValid)
            {
                User user = helperlandContext.Users.Where(user => user.Email.Equals(model.Email) && user.Password.Equals(model.Password)).FirstOrDefault();
                if(user != null)
                {
                    if(user.IsApproved)
                    {
                        message = "Login Successfully";
                    }
                    else
                    {
                        message = "Your accout is not approved by user!" +
                                  "Please Try Later!!";
                    }
                }
                else
                {
                    message = "Invalid Username or Password";
                }
                ViewBag.Alert = "<div class='alert alert-danger alert-dismissible fade show' role='alert'>" + message + "<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
                return View();
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
                    var lnkHref = "<a href='" + Url.Action("ResetPassword", "Account", new { email = model.Email, code = Token }, "http") + "'>Reset Password</a>";

                    string subject = "Reset Password Link";
                    string body = "<b>Please find the Password Reset Link. </b><br/>" + lnkHref;
                    EmailManager.SendEmail(model.Email, subject, body);
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
                    return RedirectToAction("index", "home");
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
                return RedirectToAction("index", "home");
            }
            else
            {
                return View(model);
            }
        }
    }
}
