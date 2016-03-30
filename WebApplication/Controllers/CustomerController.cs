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
                return RedirectToAction("Details", "Customer", new { id = return_status.meta.First() });
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
                return View(customer);
            }

            ReturnStatus return_status = this.customer_service.Update(customer);
            if (return_status.success)
            {
                return RedirectToAction("Details", "Customer", new { id = customer.id });
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

        [HttpGet]
        public ActionResult CreateUser(int customer_id)
        {
            Customer customer = this.customer_service.GetCustomer(customer_id);

            // Verifica se o cliente existe.
            if (customer == null)
            {
                return HttpNotFound("Cliente inexistente.");
            }

            // Verifica se o cliente está habilitado.
            if (customer.enabled == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Cliente desabilitado.");
            }

            // Verifica se o cliente já tem usuário.
            if (customer.user_id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Cliente já tem usuário.");
            }

            ViewBag.Customer = customer;

            User user = new User();
            user.profile_id = Models.Profile.CLIENTE;

            return View(user);
        }

        [HttpPost]
        public ActionResult CreateUser(int customer_id, User user)
        {
            Customer customer = this.customer_service.GetCustomer(customer_id);

            // Verifica se o cliente existe.
            if (customer == null)
            {
                return HttpNotFound("Cliente inexistente.");
            }

            // Verifica se o cliente está habilitado.
            if (customer.enabled == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Cliente desabilitado.");
            }

            // Verifica se o cliente já tem usuário.
            if (customer.user_id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Cliente já tem usuário.");
            }

            ViewBag.Customer = customer;

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            ReturnStatus return_status = this.user_service.Insert(user, customer);
            if (return_status.success)
            {
                return RedirectToAction("Details", "Customer", new { id = customer.id });
            }

            ModelState.AddModelError("", return_status.message);

            return View(user);
        }

        [HttpGet]
        public ActionResult EditUser(int customer_id)
        {
            Customer customer = this.customer_service.GetCustomer(customer_id);

            // Verifica se o cliente existe.
            if (customer == null)
            {
                return HttpNotFound("Cliente inexistente.");
            }

            // Verifica se o cliente está habilitado.
            if (customer.enabled == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Cliente desabilitado.");
            }

            ViewBag.Customer = customer;

            return View(customer.User);
        }

        [HttpPost]
        public ActionResult EditUser(int customer_id, User user)
        {
            Customer customer = this.customer_service.GetCustomer(customer_id);

            // Verifica se o cliente existe.
            if (customer == null)
            {
                return HttpNotFound("Cliente inexistente.");
            }

            // Verifica se o cliente está habilitado.
            if (customer.enabled == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Cliente desabilitado.");
            }

            ReturnStatus return_status = this.user_service.Update(user);
            if (return_status.success)
            {
                return RedirectToAction("Details", "Customer", new { id = customer.id });
            }

            ViewBag.Customer = customer;

            if (!ModelState.IsValid)
            {
                return View();
            }

            ModelState.AddModelError("", return_status.message);

            return View(user);
        }
    }
}
