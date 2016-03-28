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
    public class OrderController : AppController
    {
        private IOrderService order_service;
        private IPaymentTypeService payment_type_service;
        private IPizzaSizeService pizza_size_service;

        public OrderController()
        {
            this.order_service = new OrderService(new OrderRepository());
            this.payment_type_service = new PaymentTypeService(new PaymentTypeRepository());
            this.pizza_size_service = new PizzaSizeService(new PizzaSizeRepository());
        }

        public OrderController(IOrderService order_service)
        {
            this.order_service = order_service;
            this.payment_type_service = new PaymentTypeService(new PaymentTypeRepository());
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(this.order_service.GetOrdersToday());
        }

        [HttpGet]
        public ActionResult Create(int customer_id)
        {
            ReturnStatus return_status = new ReturnStatus();

            // Verifica se o cliente já tem um pedido abrindo.
            Order order = this.order_service.GetLastOrder(customer_id);
            if (order != null)
            {
                // Redirecionado para o detalhamento do pedido.
                if ((order.LastOrderLog.order_status_id == Models.OrderStatus.ABRINDO) && (order.order_date == DateTime.Today))
                {
                    return RedirectToAction("Details", "Order", new { id = order.id });
                }
            }

            return_status = this.order_service.Insert(customer_id, CurrentUser);

            if (!return_status.success)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, return_status.message);
            }

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult Details(int id)
        {
            Order order = this.order_service.GetOrder(id);

            if (order == null)
            {
                return HttpNotFound("Pedido não encontrado.");
            }

            return View(order);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Order order = this.order_service.GetOrder(id);

            if (order == null)
            {
                return HttpNotFound("Pedido não encontrado.");
            }

            if (order.LastOrderLog.OrderStatus.id != Models.OrderStatus.ABRINDO)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "O pedido já passou do status abrindo.");
            }

            ViewBag.payment_type_id = new SelectList
                (
                    this.payment_type_service.GetPaymentTypes(),
                    "id",
                    "name",
                    Convert.ToString(order.payment_type_id)
                );

            ViewBag.pizza_id = new SelectList
                (
                    this.pizza_size_service.GetPizzaSizes(),
                    "id",
                    "PizzaFlavor.name"
                );

            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(Order order, string payment_type_id)
        {
            ViewBag.payment_type_id = new SelectList
                (
                    this.payment_type_service.GetPaymentTypes(),
                    "id",
                    "name",
                    payment_type_id
                );

            ViewBag.pizza_size_id = new SelectList
                (
                    this.pizza_size_service.GetPizzaSizes(),
                    "id",
                    "name"
                );

            if (!ModelState.IsValid)
            {
                return View(order);
            }

            if (payment_type_id != "")
            {
                order.payment_type_id = Convert.ToInt32(payment_type_id);
            }

            ReturnStatus return_status = this.order_service.Update(order);
            if (return_status.success)
            {
                return RedirectToAction("Details", "Order", new { id = order.id });
            }

            ModelState.AddModelError("", return_status.message);

            return View(order);
        }

        [AllowAnonymous]
        public ActionResult DetailsPDF(int id)
        {
            return new Rotativa.ActionAsPdf("Details", new { id = id }) { FileName = String.Format("Pedido{0}.pdf", id) };
        }
    }
}
