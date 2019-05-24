using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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

        public OrderController()
        {
            this.order_service = new OrderService(new OrderRepository());
            this.payment_type_service = new PaymentTypeService(new PaymentTypeRepository());
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

            return RedirectToAction("Details", "Order", new { id = return_status.meta.First() });
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Order order = this.order_service.GetOrder(id);

            if (order == null)
            {
                return HttpNotFound("Pedido não encontrado.");
            }

            ViewBag.Modify = ((order.LastOrderLog.OrderStatus.id == Models.OrderStatus.ABRINDO) ? true : false);

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

            List<SelectListItem> customer_address_items = new List<SelectListItem>();
            foreach (var customer_address in order.CustomerAddress.Customer.CustomerAddress)
            {
                customer_address_items.Add(new SelectListItem { Value = Convert.ToString(customer_address.id), Text = String.Format("{0}, {1}, {2}, {3}", customer_address.address, customer_address.number, customer_address.complement, customer_address.neighborhood), Selected = ((order.customer_address_id == customer_address.id) ? true : false) });
            }
            ViewBag.customer_address_id = customer_address_items;

            return View(order);
        }

        [HttpPost]
        public ActionResult Edit(Order order, string payment_type_id, string customer_address_id)
        {
            ViewBag.payment_type_id = new SelectList
                (
                    this.payment_type_service.GetPaymentTypes(),
                    "id",
                    "name",
                    payment_type_id
                );

            Order _order = this.order_service.GetOrder(order.id);
            List<SelectListItem> customer_address_items = new List<SelectListItem>();
            foreach (var customer_address in _order.CustomerAddress.Customer.CustomerAddress)
            {
                customer_address_items.Add(new SelectListItem { Value = Convert.ToString(customer_address.id), Text = String.Format("{0}, {1}, {2}, {3}", customer_address.address, customer_address.number, customer_address.complement, customer_address.neighborhood), Selected = ((order.customer_address_id == customer_address.id) ? true : false) });
            }
            ViewBag.customer_address_id = customer_address_items;

            if (!ModelState.IsValid)
            {
                return View(order);
            }

            if (payment_type_id != "")
            {
                order.payment_type_id = Convert.ToInt32(payment_type_id);
            }

            if (customer_address_id != "")
            {
                order.customer_address_id = Convert.ToInt32(customer_address_id);
            }

            ReturnStatus return_status = this.order_service.Update(order);
            if (return_status.success)
            {
                return RedirectToAction("Details", "Order", new { id = order.id });
            }

            ModelState.AddModelError("", return_status.message);

            return View(order);
        }

        [HttpGet]
        public ActionResult DeliveredToday()
        {
            return View(this.order_service.GetTotalOrdersDeliveredToday());
        }

        public ActionResult Download(int id)
        {
            MemoryStream memoryStream = new MemoryStream();
            TextWriter tw = new StreamWriter(memoryStream);

            Order order = this.order_service.GetOrder(id);

            tw.WriteLine(String.Format("Data: {0}", order.order_date.ToString("dd/MM/yyyy")));
            tw.WriteLine(String.Format("Código: {0}", order.id));            
            tw.WriteLine(String.Format("Cliente: {0}", order.CustomerAddress.Customer.name));
            tw.WriteLine(String.Format("Telefone: {0}", order.CustomerAddress.Customer.phone));
            tw.WriteLine(String.Format("Endereço: {0}, {1}, {2}, {3}, {4}/{5}, {6}", order.CustomerAddress.address, order.CustomerAddress.number, order.CustomerAddress.complement, order.CustomerAddress.neighborhood, order.CustomerAddress.city, order.CustomerAddress.acronym_city, order.CustomerAddress.zip_code));
            tw.WriteLine(String.Format("Referência: {0}", order.CustomerAddress.reference_point));
            tw.WriteLine(String.Format("OBS: {0}", order.note));

            tw.WriteLine("");
            int i = 1;
            foreach (var order_pizza in order.OrderPizza)
            {
                tw.WriteLine(String.Format("Item {0}: {1} {2} de {3} ({4})", i, order_pizza.amount, order_pizza.Pizza.PizzaSize.name, order_pizza.Pizza.PizzaFlavor.name, order_pizza.note));
                i++;
            }

            foreach (var order_drink in order.OrderDrink)
            {
                tw.WriteLine(String.Format("Item {0}: {1}", i, order_drink.Drink.name));
                i++;
            }            

            tw.WriteLine("");
            tw.WriteLine(String.Format("Forma de Pagamento: {0}", order.PaymentType.name));
            tw.WriteLine(String.Format("Preço: R$ {0}", order.price));
            tw.WriteLine(String.Format("Desconto: R$ {0}", order.discount));
            tw.WriteLine(String.Format("Taxa de Entrega: R$ {0} ", order.delivery_price));
            tw.WriteLine(String.Format("Preço Final: R$ {0}", order.final_price));
            tw.WriteLine(String.Format("Pagamento: R$ {0}", order.payment));
            tw.WriteLine(String.Format("Troco: R$ {0}", order.change));            

            tw.Flush();
            tw.Close();

            return File(memoryStream.GetBuffer(), "text/plain", String.Format("Pedido{0}.txt", order.id));
        }

        [HttpGet]
        public ActionResult CreateII()
        {
            return View();
        }
    }
}
