using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Services;
using WebApplication.Repositories;
using System.Security.Claims;
using System.Net;

namespace WebApplication.Controllers
{
    public class HomeController : AppController
    {
        private IProfileService profile_service;
        private IUserService user_service;

        public HomeController()
        {
            this.profile_service = new ProfileService(new ProfileRepository());
            this.user_service = new UserService(new UserRepository());
        }

        public HomeController(IProfileService profile_service, IUserService user_service)
        {
            this.profile_service = profile_service;
            this.user_service = user_service;
        }
                
        public ActionResult Index()
        {            
            ViewBag.Title = "Pizzaria Sognare";

            ViewBag.CurrentUser = CurrentUser;                                  

            return View();
        }       
    }
}
