using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Repositories;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class DrinkController : AppController
    {
        private IDrinkService drink_service;

        public DrinkController()
        {
            this.drink_service = new DrinkService(new DrinkRepository());
        }

        public DrinkController(IDrinkService drink_service)
        {
            this.drink_service = drink_service;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            return View(this.drink_service.GetDrinks(true));
        }
    }
}
