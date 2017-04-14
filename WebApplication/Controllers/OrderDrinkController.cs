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
    public class OrderDrinkController : AppController
    {
        private IOrderDrinkService order_drink_service;
        private IOrderService order_sevice;
        private IDrinkService drink_service;

        public OrderDrinkController()
        {
            this.order_drink_service = new OrderDrinkService(new OrderDrinkRepository());
            this.order_sevice = new OrderService(new OrderRepository());
            this.drink_service = new DrinkService(new DrinkRepository());
        }

        public OrderDrinkController(IOrderDrinkService order_drink_service)
        {
            this.order_drink_service = order_drink_service;
        }

        [HttpGet]
        public ActionResult Create(int order_id)
        {
            Order order = this.order_sevice.GetOrder(order_id);

            if (order == null)
            {
                return HttpNotFound("Pedido não encontrado.");
            }

            if (order.LastOrderLog.OrderStatus.id != Models.OrderStatus.ABRINDO)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "O pedido já passou do status abrindo.");
            }

            ViewBag.Order = order;

            List<Drink> drinks = this.drink_service.GetDrinks(true);
            List<SelectListItem> drinks_items = new List<SelectListItem>();
            foreach (var drink in drinks)
            {
                drinks_items.Add(new SelectListItem { Value = Convert.ToString(drink.id), Text = String.Format("{0}", drink.name) });
            }
            ViewBag.drink_id = drinks_items;

            OrderDrink order_drink = new OrderDrink();
            order_drink.order_id = order.id;

            return View(order_drink);
        }

        [HttpPost]
        public ActionResult Create(OrderDrink order_drink, string drink_id)
        {
            Order order = this.order_sevice.GetOrder(order_drink.order_id);

            if (order == null)
            {
                return HttpNotFound("Pedido não encontrado.");
            }

            if (order.LastOrderLog.OrderStatus.id != Models.OrderStatus.ABRINDO)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "O pedido já passou do status abrindo.");
            }

            ViewBag.Order = order;

            List<Drink> drinks = this.drink_service.GetDrinks(true);
            List<SelectListItem> drinks_items = new List<SelectListItem>();
            foreach (var drink in drinks)
            {
                drinks_items.Add(new SelectListItem { Value = Convert.ToString(drink.id), Text = String.Format("{0}", drink.name) });
            }
            ViewBag.Drink_id = drinks_items;

            order_drink.drink_id = Convert.ToInt32(drink_id);

            if (!ModelState.IsValid)
            {
                return View(order_drink);
            }

            ReturnStatus return_status = this.order_drink_service.Insert(order_drink);
            if (return_status.success)
            {
                return RedirectToAction("Details", "Order", new { id = order_drink.order_id });
            }

            ModelState.AddModelError("", return_status.message);

            return View(order_drink);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            OrderDrink order_drink = this.order_drink_service.GetOrderDrink(id);

            if (order_drink == null)
            {
                return HttpNotFound("Bebida não encontrada no pedido.");
            }

            Order order = this.order_sevice.GetOrder(order_drink.order_id);

            if (order == null)
            {
                return HttpNotFound("Pedido não encontrado.");
            }

            if (order.LastOrderLog.OrderStatus.id != Models.OrderStatus.ABRINDO)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "O pedido já foi fechado.");
            }

            this.order_drink_service.Delete(order_drink);

            return RedirectToAction("Details", "Order", new { id = order_drink.order_id });
        }
    }
}
