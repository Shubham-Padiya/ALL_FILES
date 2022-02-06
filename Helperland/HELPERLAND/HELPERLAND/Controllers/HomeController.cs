using HELPERLAND.Models;
using HELPERLAND.Models.Data;
using HELPERLAND.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HELPERLAND.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HelperlandContext helperlandContext;

        public HomeController(ILogger<HomeController> logger , HelperlandContext _helperlandContext)
        {
            _logger = logger;
            helperlandContext = _helperlandContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Become_a_Provider()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Become_a_Provider(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var Emailisalreadyexists = helperlandContext.Users.Any(user => user.Email == model.Email);
                if (Emailisalreadyexists)
                {
                    ViewBag.Alert = "<div class='alert alert-danger alert-dismissible fade show' role='alert'>Service Provider with this email already exists<button type= 'button' class='btn-close' data-bs-dismiss='alert' aria-label='Close'></button></div>";
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
                        UserTypeId = 2,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
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
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]

        public IActionResult ContactUs(ContactUsViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.File != null)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\ContactUsAttechment");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.File.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                ContactUs contact = new ContactUs()
                {
                    Name = model.FirstName + " " + model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.Mobile,
                    Message = model.Message,
                    Subject = model.Subject,
                    UploadFileName = model.File.FileName,
                    CreatedOn = DateTime.Now,
                    FileName = uniqueFileName
                };
                helperlandContext.ContactUs.Add(contact);
                helperlandContext.SaveChanges();
                return Json("Query submitted successfully..");
            }
            else
            {
                return Json(ModelState.Values);
            }
        }


        

        public IActionResult FAQs()
        {
            return View();
        }

        public IActionResult Prices()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
