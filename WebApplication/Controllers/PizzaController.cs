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
    public class PizzaFlavorController : AppController
    {
        private IPizzaFlavorService pizza_flavor_service;

        public PizzaFlavorController()
        {
            this.pizza_flavor_service = new PizzaFlavorService(new PizzaFlavorRepository());
        }

        public PizzaFlavorController(IPizzaFlavorService pizza_flavor_service)
        {
            this.pizza_flavor_service = pizza_flavor_service;
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            return View(this.pizza_flavor_service.GetPizzaFlavors());
        }
    }
}
