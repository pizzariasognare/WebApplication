using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public interface IOrderService
    {
        /// <summary>
        /// Método retorna uma lista de pedidos.
        /// </summary>
        /// <returns>Lista de pedidos.</returns>
        List<Order> GetOrders();

        /// <summary>
        /// Método retorna um pedido.
        /// </summary>
        /// <param name="id">Idenficador do pedido</param>
        /// <returns>Objeto pedido</returns>
        Order GetOrder(int id);

        /// <summary>
        /// Método retorna o último pedido do cliente.
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Objeto</returns>
        Order GetLastOrder(int customer_id);

        /// <summary>
        /// Método retorna uma lista de pedidos de hoje.
        /// </summary>
        /// <returns>Lista de pedidos de hoje.</returns>
        List<Order> GetOrdersToday();

        /// <summary>
        /// Método insere um pedido.
        /// </summary>        
        /// <param name="customer_id">Identificador do cliente.</param>
        /// <param name="current_user">Usuário logado</param>
        /// <returns>Objeto</returns>
        ReturnStatus Insert(int customer_id, AppUser current_user);

        /// <summary>
        /// Método atualiza  um pedido.
        /// </summary>
        /// <param name="order">Objeto pedido</param>        
        /// <returns>Objeto</returns>
        ReturnStatus Update(Order order);

        /// <summary>
        /// Método retorna o total de pedidos entregues por funciónário.
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Lista de total de pedidos</returns>
        List<TotalOrdersDeliveredToday> GetTotalOrdersDeliveredToday();
    }

    public class OrderService : IOrderService
    {
        private IOrderRepository order_repository;
        private ICustomerAddressService customer_address_service;
        private IOrderLogService order_log_service;

        public OrderService(IOrderRepository order_repository)
        {
            this.order_repository = order_repository;
            this.customer_address_service = new CustomerAddressService(new CustomerAddressRepository());
            this.order_log_service = new OrderLogService(new OrderLogRepository());
        }

        /// <summary>
        /// Método retorna uma lista de pedidos.
        /// </summary>
        /// <returns>Lista de pedidos.</returns>
        public List<Order> GetOrders()
        {
            return this.order_repository.GetOrders();
        }

        /// <summary>
        /// Método retorna um pedido.
        /// </summary>
        /// <param name="id">Identificador do pedido</param>
        /// <returns>Objeto</returns>
        public Order GetOrder(int id)
        {
            return this.order_repository.GetOrder(id);
        }

        /// <summary>
        /// Método retorna o pedido.
        /// </summary>
        /// <param name="id">Identificador do pedido.</param>
        /// <returns>Objeto</returns>
        public Order GetLastOrder(int customer_id)
        {
            return this.order_repository.GetLastOrder(customer_id);
        }

        /// <summary>
        /// Método retorna uma lista de pedidos.
        /// </summary>
        /// <returns>Lista de pedidos.</returns>
        public List<Order> GetOrdersToday()
        {
            return this.order_repository.GetOrdersToday();
        }

        /// <summary>
        /// Método insere um pedido.
        /// </summary>
        /// <param name="customer_id">Identificador do cliente </param>
        /// <returns>Objeto</returns>
        public ReturnStatus Insert(int customer_id, AppUser current_user)
        {
            ReturnStatus return_status = new ReturnStatus();            
                        
            // Verifica se o existe o endereço do cliente.
            CustomerAddress customer_address = this.customer_address_service.GetLastCustomerAddress(customer_id);
            if (customer_address == null)
            {
                return_status.message = "Cliente não tem endereço cadastrado.";
                return return_status;
            }

            Order order = new Order();
            order.customer_address_id = customer_address.id;
            order.payment_type_id = Models.PaymentType.DINHEIRO;
            order.order_date = DateTime.Today;
            order.delivery_price = customer_address.ZipCode.delivery_price;
            order.final_price = customer_address.ZipCode.delivery_price;   

            // Insere o pedido.
            int order_id_inserted = this.order_repository.Insert(order);
            if (order_id_inserted == 0)
            {
                return_status.message = "Erro ao adicionar pedido.";
                return return_status;
            }

            OrderLog order_log = new OrderLog();
            order_log.order_id = order_id_inserted;
            order_log.order_status_id = Models.OrderStatus.ABRINDO;
            order_log.user_id = current_user.id;
            order_log.order_log_datetime = DateTime.Now;

            // Insere o log do pedido
            return_status = this.order_log_service.Insert(order_log);
            if (!return_status.success)
            {
                return return_status;
            }

            return_status.success = true;
            return_status.message = "Pedido adicionado com sucesso.";
            return_status.meta.Add(Convert.ToString(order_id_inserted));
            return return_status;
        }

        /// <summary>
        /// Método atualiza um pedido.
        /// </summary>
        /// <param name="order">Objeto pedido</param>
        /// <returns>Objeto</returns>
        public ReturnStatus Update(Order order)
        {
            ReturnStatus return_status = new ReturnStatus();

            // Atualiza o pedido.            
            if (!this.order_repository.Update(order))
            {
                return_status.message = "Erro ao atualizar pedido.";
                return return_status;
            }

            return_status.success = true;
            return_status.message = "Pedido atualizado com sucesso.";
            return return_status;
        }

        /// <summary>
        /// Método retorna o total de pedidos entregues por funciónário.
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Lista de total de pedidos</returns>
        public List<TotalOrdersDeliveredToday> GetTotalOrdersDeliveredToday()
        {
            return this.order_repository.GetTotalOrdersDeliveredToday();
        }
    }
}