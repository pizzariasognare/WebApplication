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
    public class CustomerController : AppController
    {
        private ICustomerService customer_service;
        private IUserService user_service;

        public CustomerController()
        {
            this.customer_service = new CustomerService(new CustomerRepository());
            this.user_service = new UserService(new UserRepository());
        }

        public CustomerController(ICustomerService customer_service, IUserService user_service)
        {
            this.customer_service = customer_service;
            this.user_service = user_service;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(this.customer_service.GetCustomers());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ReturnStatus return_status = this.customer_service.Insert(customer);
            if (return_status.success)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", return_status.message);

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Customer customer = this.customer_service.GetCustomer(id);

            if (customer == null) 
            {
                return HttpNotFound("Cliente não encontrado.");
            }

            return View(customer);
        }

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ReturnStatus return_status = this.customer_service.Update(customer);
            if (return_status.success)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", return_status.message);
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Customer customer = this.customer_service.GetCustomer(id);

            if (customer == null)
            {
                return HttpNotFound("Cliente não encontrado.");
            }

            return View(customer);
        }

        [HttpPost]
        public JsonResult SetEnabled(int id, short value)
        {
            ReturnStatus return_status = this.customer_service.SetEnabled(id, value);

            return Json(return_status);
        }
    }
}
