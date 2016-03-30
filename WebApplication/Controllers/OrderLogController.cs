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
    public class OrderLogController : AppController
    {
        private IOrderLogService order_log_service;
        private IOrderService order_sevice;
        private IOrderStatusService order_status_service;
        private IEmployerService employer_service;

        public OrderLogController()
        {
            this.order_log_service = new OrderLogService(new OrderLogRepository());
            this.order_sevice = new OrderService(new OrderRepository());
            this.order_status_service = new OrderStatusService(new OrderStatusRepository());
            this.employer_service = new EmployerService(new EmployerRepository());
        }

        public OrderLogController(IOrderLogService order_log_service)
        {
            this.order_log_service = order_log_service;
        }

        [HttpGet]
        public ActionResult Create(int order_id)
        {
            Order order = this.order_sevice.GetOrder(order_id);

            if (order == null)
            {
                return HttpNotFound("Pedido não encontrado.");
            }

            ViewBag.Order = order;

            List<OrderStatus> order_status = this.order_status_service.GetNextOrderStatus(order.LastOrderLog.order_status_id);
            List<SelectListItem> order_status_items = new List<SelectListItem>();
            foreach (var order_status_item in order_status)
            {
                order_status_items.Add(new SelectListItem { Value = Convert.ToString(order_status_item.id), Text = String.Format("{0}", order_status_item.description) });
            }
            ViewBag.order_status_id = order_status_items;

            List<Employer> employers = this.employer_service.GetEmployers();
            List<SelectListItem> employers_items = new List<SelectListItem>();
            foreach (var employer in employers)
            {
                employers_items.Add(new SelectListItem { Value = Convert.ToString(employer.user_id), Text = String.Format("{0}", employer.name) });
            }
            ViewBag.user_id = employers_items;

            OrderLog order_log = new OrderLog();
            order_log.order_id = order.id;

            return View(order_log);
        }

        [HttpPost]
        public ActionResult Create(OrderLog order_log, string order_status_id, string user_id)
        {
            Order order = this.order_sevice.GetOrder(order_log.order_id);

            if (order == null)
            {
                return HttpNotFound("Pedido não encontrado.");
            }

            ViewBag.Order = order;

            List<OrderStatus> order_status = this.order_status_service.GetNextOrderStatus(order.LastOrderLog.order_status_id);
            List<SelectListItem> order_status_items = new List<SelectListItem>();
            foreach (var order_status_item in order_status)
            {
                order_status_items.Add(new SelectListItem { Value = Convert.ToString(order_status_item.id), Text = String.Format("{0}", order_status_item.description) });
            }
            ViewBag.order_status_id = order_status_items;

            List<Employer> employers = this.employer_service.GetEmployers();
            List<SelectListItem> employers_items = new List<SelectListItem>();
            foreach (var employer in employers)
            {
                employers_items.Add(new SelectListItem { Value = Convert.ToString(employer.user_id), Text = String.Format("{0}", employer.name) });
            }
            ViewBag.user_id = employers_items;

            order_log.order_status_id = Convert.ToInt32(order_status_id);
            order_log.user_id = Convert.ToInt32(user_id);
            order_log.order_log_datetime = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return View(order_log);
            }

            ReturnStatus return_status = this.order_log_service.Insert(order_log);
            if (return_status.success)
            {
                return RedirectToAction("Details", "Order", new { id = order_log.order_id });
            }

            ModelState.AddModelError("", return_status.message);

            return View(order_log);
        }
    }
}
