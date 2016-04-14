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
    public class OrderPizzaController : AppController
    {
        private IOrderPizzaService order_pizza_service;
        private IOrderService order_sevice;
        private IPizzaService pizza_service;

        public OrderPizzaController()
        {
            this.order_pizza_service = new OrderPizzaService(new OrderPizzaRepository());
            this.order_sevice = new OrderService(new OrderRepository());
            this.pizza_service = new PizzaService(new PizzaRepository());
        }

        public OrderPizzaController(IOrderPizzaService order_pizza_service)
        {
            this.order_pizza_service = order_pizza_service;
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

            List<Pizza> pizzas = this.pizza_service.GetPizzas();
            List<SelectListItem> pizzas_items = new List<SelectListItem>();
            foreach (var pizza in pizzas)
            {
                pizzas_items.Add(new SelectListItem { Value = Convert.ToString(pizza.id), Text = String.Format("{0} de {1}", pizza.PizzaSize.name, pizza.PizzaFlavor.name) });
            }
            ViewBag.pizza_id = pizzas_items;

            OrderPizza order_pizza = new OrderPizza();
            order_pizza.amount = 1;
            order_pizza.order_id = order.id;

            return View(order_pizza);
        }

        [HttpPost]
        public ActionResult Create(OrderPizza order_pizza, string pizza_id)
        {
            Order order = this.order_sevice.GetOrder(order_pizza.order_id);

            if (order == null)
            {
                return HttpNotFound("Pedido não encontrado.");
            }

            if (order.LastOrderLog.OrderStatus.id != Models.OrderStatus.ABRINDO)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "O pedido já passou do status abrindo.");
            }

            ViewBag.Order = order;

            List<Pizza> pizzas = this.pizza_service.GetPizzas();
            List<SelectListItem> pizzas_items = new List<SelectListItem>();
            foreach (var pizza in pizzas)
            {
                pizzas_items.Add(new SelectListItem { Value = Convert.ToString(pizza.id), Text = String.Format("{0} de {1}", pizza.PizzaSize.name, pizza.PizzaFlavor.name) });
            }
            ViewBag.pizza_id = pizzas_items;

            order_pizza.pizza_id = Convert.ToInt32(pizza_id);

            if (!ModelState.IsValid)
            {
                return View(order_pizza);
            }

            ReturnStatus return_status = this.order_pizza_service.Insert(order_pizza);
            ModelState.AddModelError("", return_status.message);

            return View(order_pizza);
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            OrderPizza order_pizza = this.order_pizza_service.GetOrderPizza(id);

            if (order_pizza == null)
            {
                return HttpNotFound("Pizza não encontrada no pedido.");
            }

            Order order = this.order_sevice.GetOrder(order_pizza.order_id);

            if (order == null)
            {
                return HttpNotFound("Pedido não encontrado.");
            }

            if (order.LastOrderLog.OrderStatus.id != Models.OrderStatus.ABRINDO)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "O pedido já foi fechado.");
            }

            this.order_pizza_service.Delete(order_pizza);

            return RedirectToAction("Details", "Order", new { id = order_pizza.order_id });
        }
    }
}
