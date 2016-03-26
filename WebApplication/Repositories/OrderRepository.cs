using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Método retorna uma lista de pedidos.
        /// </summary>
        /// <returns>Lista de pedidos.</returns>
        List<Order> GetOrders();

        /// <summary>
        /// Método retorna uma lista de pedidos de hoje.
        /// </summary>
        /// <returns>Lista de pedidos de hoje.</returns>
        List<Order> GetOrdersToday();

        /// <summary>
        /// Método retorna o pedido.
        /// </summary>
        /// <param name="id">Identificador do pedido.</param>
        /// <returns>Objeto</returns>
        Order GetOrder(int id);

        /// <summary>
        /// Método retorna o último pedido do cliente.
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Objeto</returns>
        Order GetLastOrder(int customer_id);

        /// <summary>
        /// Método insere um pedido.
        /// </summary>
        /// <param name="order">Objeto pedido</param>
        /// <returns>Id do pedido inserido.</returns>        
        int Insert(Order order);

        /// <summary>
        /// Método atualiza um pedido.
        /// </summary>
        /// <param name="order">Objeto</param>
        /// <returns>Status da atualização (Verdade ou falso)</returns>
        bool Update(Order order);
    }

    public class OrderRepository : IOrderRepository
    {
        private ICustomerAddressRepository customer_address_repository;
        private IPaymentTypeRepository payment_type_repository;
        private IOrderLogRepository order_log_repository;
        private IOrderPizzaRepository order_pizza_repository;

        public OrderRepository()
        {
            this.customer_address_repository = new CustomerAddressRepository();
            this.payment_type_repository = new PaymentTypeRepository();
            this.order_log_repository = new OrderLogRepository();
            this.order_pizza_repository = new OrderPizzaRepository();
        }

        /// <summary>
        /// Método retorna uma lista de pedidos.
        /// </summary>
        /// <returns>Lista de pedidos.</returns>
        public List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();

            using (Entities entities = new Entities())
            {
                List<Order> _orders = entities.Order.OrderByDescending(o => o.id).ToList();

                foreach (var order in _orders)
                {
                    if (order.payment_type_id.HasValue)
                    {
                        order.PaymentType = this.payment_type_repository.GetPaymentType(order.payment_type_id.Value);
                    }

                    order.CustomerAddress = this.customer_address_repository.GetCustomerAddress(order.customer_address_id);

                    order.LastOrderLog = this.order_log_repository.GetLastOrderLog(order.id);

                    orders.Add(order);
                }
            }

            return orders;
        }

        /// <summary>
        /// Método retorna uma lista de pedidos de hoje.
        /// </summary>
        /// <returns>Lista de pedidos de hoje</returns>
        public List<Order> GetOrdersToday()
        {
            List<Order> orders = new List<Order>();

            using (Entities entities = new Entities())
            {
                List<Order> _orders = entities.Order.Where(o => o.order_date == DateTime.Today).OrderByDescending(o => o.id).ToList();

                foreach (var order in _orders)
                {
                    if (order.payment_type_id.HasValue)
                    {
                        order.PaymentType = this.payment_type_repository.GetPaymentType(order.payment_type_id.Value);
                    }

                    order.CustomerAddress = this.customer_address_repository.GetCustomerAddress(order.customer_address_id);

                    order.LastOrderLog = this.order_log_repository.GetLastOrderLog(order.id);

                    orders.Add(order);
                }
            }

            return orders;
        }

        /// <summary>
        /// Método retorna um pedido.
        /// </summary>
        /// <param name="id">Identificador do pedido..</param>
        /// <returns>Objeto</returns>
        public Order GetOrder(int id)
        {
            Order order = new Order();

            using (Entities entities = new Entities())
            {
                order = entities.Order.Where(p => p.id == id).FirstOrDefault();

                if (order != null)
                {
                    if (order.payment_type_id.HasValue)
                    {
                        order.PaymentType = this.payment_type_repository.GetPaymentType(order.payment_type_id.Value);
                    }

                    order.CustomerAddress = this.customer_address_repository.GetCustomerAddress(order.customer_address_id);

                    order.LastOrderLog = this.order_log_repository.GetLastOrderLog(order.id);

                    order.OrderPizza = this.order_pizza_repository.GetOrderPizzas(order.id);
                }
            }

            return order;
        }

        /// <summary>
        /// Método retorna o último pedido do cliente.
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Objeto</returns>
        public Order GetLastOrder(int customer_id)
        {
            Order order = new Order();

            using (Entities entities = new Entities())
            {
                order = (from O in entities.Order
                         join CA in entities.CustomerAddress on O.customer_address_id equals CA.id
                         where CA.customer_id == customer_id
                         select O).OrderByDescending(o => o.id).FirstOrDefault();

                if (order != null)
                {
                    order.CustomerAddress = this.customer_address_repository.GetCustomerAddress(order.customer_address_id);

                    order.LastOrderLog = this.order_log_repository.GetLastOrderLog(order.id);

                    order.OrderPizza = this.order_pizza_repository.GetOrderPizzas(order.id);
                }
            }

            return order;
        }



        /// <summary>
        /// Método insere uma pizza no pedido.
        /// </summary>
        /// <param name="OrderPizza">Objeto</param>
        /// <returns>Status da inserção (Verdade ou falso)</returns>
        public int Insert(Order order)
        {
            try
            {
                int id = 0;

                using (Entities entities = new Entities())
                {
                    entities.Order.Add(order);
                    entities.SaveChanges();
                    id = order.id;
                }

                return id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método atualiza uma pizza no pedido.
        /// </summary>
        /// <param name="OrderPizza">Objeto</param>
        /// <returns>Id do pedido inserido.</returns>
        public bool Update(Order order)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    entities.Entry(order).State = EntityState.Modified;
                    entities.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}