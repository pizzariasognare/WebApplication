﻿using System;
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

            ViewBag.Customer = customer;

            CustomerAddress customer_address = new CustomerAddress();
            customer_address.customer_id = customer.id;
            customer_address.city = "Rio de Janeiro";
            customer_address.acronym_city = "RJ";

            return View(customer_address);
        }

        [HttpPost]
        public ActionResult Create(CustomerAddress customer_address)
        {
            Customer customer = this.customer_service.GetCustomer(customer_address.customer_id);

            if (customer == null)
            {
                return HttpNotFound("Cliente não encontrado.");
            }

            if (customer.enabled == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Cliente desabilitado.");
            }

            ViewBag.Customer = customer;

            if (!ModelState.IsValid)
            {
                return View();
            }

            ReturnStatus return_status = this.customer_address_service.Insert(customer_address);
            if (return_status.success)
            {
                return RedirectToAction("Details", "Customer", new { id = customer_address.customer_id });
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

            Customer customer = this.customer_service.GetCustomer(customer_address.customer_id);

            if (customer == null)
            {
                return HttpNotFound("Cliente não encontrado.");
            }

            if (customer.enabled == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Cliente desabilitado.");
            }

            ViewBag.Customer = customer;

            return View(customer_address);
        }

        [HttpPost]
        public ActionResult Edit(CustomerAddress customer_address)
        {
            Customer customer = this.customer_service.GetCustomer(customer_address.customer_id);

            if (customer == null)
            {
                return HttpNotFound("Cliente não encontrado.");
            }

            if (customer.enabled == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Cliente desabilitado.");
            }

            ViewBag.Customer = customer;

            if (!ModelState.IsValid)
            {
                return View();
            }

            ReturnStatus return_status = this.customer_address_service.Update(customer_address);
            if (return_status.success)
            {
                return RedirectToAction("Details", "Customer", new { id = customer_address.customer_id });
            }

            ModelState.AddModelError("", return_status.message);

            return View();
        }
    }
}
