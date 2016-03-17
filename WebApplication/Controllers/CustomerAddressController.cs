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
    public class CustomerAddressController : AppController
    {
        private ICustomerAddressService customer_address_service;
        private ICustomerService customer_service;

        public CustomerAddressController()
        {
            this.customer_address_service = new CustomerAddressService(new CustomerAddressRepository());
            this.customer_service = new CustomerService(new CustomerRepository());
        }

        public CustomerAddressController(ICustomerAddressService customer_address_service, ICustomerService customer_service)
        {
            this.customer_address_service = customer_address_service;
            this.customer_service = customer_service;
        }

        [HttpGet]
        public ActionResult Create(int customer_id)
        {
            Customer customer = this.customer_service.GetCustomer(customer_id);

            if (customer == null)
            {
                return HttpNotFound("Cliente não encontrado.");
            }

            if (customer.enabled == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Cliente desabilitado.");
            }

            CustomerAddress customer_address = new CustomerAddress();
            customer_address.customer_id = customer.id;

            ViewBag.Customer = customer;

            return View(customer_address);
        }

        [HttpPost]
        public ActionResult Create(CustomerAddress CustomerAddress)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ReturnStatus return_status = this.customer_address_service.Insert(CustomerAddress);
            if (return_status.success)
            {
                return RedirectToAction("Details", "Customer", new { id = CustomerAddress.customer_id });
            }

            ModelState.AddModelError("", return_status.message);

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            CustomerAddress customer_address = this.customer_address_service.GetCustomerAddress(id);

            if (customer_address == null)
            {
                return HttpNotFound("Endereço do cliente não encontrado.");
            }

            ViewBag.Customer = this.customer_service.GetCustomer(customer_address.customer_id);

            return View(customer_address);
        }

        [HttpPost]
        public ActionResult Edit(CustomerAddress CustomerAddress)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ReturnStatus return_status = this.customer_address_service.Update(CustomerAddress);
            if (return_status.success)
            {
                return RedirectToAction("Details", "Customer", new { id = CustomerAddress.customer_id });
            }

            ModelState.AddModelError("", return_status.message);

            return View();
        }
    }
}
