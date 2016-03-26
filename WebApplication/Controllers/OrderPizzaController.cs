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

            ViewBag.Order = order;

            List<Pizza> pizzas = this.pizza_service.GetPizzas();
            List<SelectListItem> pizzas_items = new List<SelectListItem>();
            foreach (var pizza in pizzas)
            {
                pizzas_items.Add(new SelectListItem { Value = Convert.ToString(pizza.id), Text = String.Format("{0} de {1}", pizza.PizzaSize.name, pizza.PizzaFlavor.name)});
            }
            ViewBag.pizza_id = pizzas_items;

            OrderPizza order_pizza = new OrderPizza();
            order_pizza.order_id = order.id;
            order_pizza.amount = 1;

            return View(order_pizza);
        }

        [HttpPost]
        public ActionResult Create(OrderPizza order_pizza, string pizza_flavor_id)
        {
            Order order = this.order_sevice.GetOrder(order_pizza.order_id);

            if (order == null)
            {
                return HttpNotFound("Pedido não encontrado.");
            }

            ViewBag.Order = order;

            ViewBag.pizza_id = new SelectList
                (
                    this.pizza_service.GetPizzas(),
                    "id",
                    "pizza_flavor_id"
                );

            return View(order_pizza);
        }
    }
}
