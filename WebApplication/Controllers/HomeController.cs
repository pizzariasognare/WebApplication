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
        public HomeController()
        {            
        }
                
        public ActionResult Index()
        {            
            ViewBag.Title = "Pizzaria Sognare";

            return View();
        }       
    }
}
