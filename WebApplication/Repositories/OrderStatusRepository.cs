using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IOrderStatusRepository
    {
        /// <summary>
        /// Método retorna uma lista de status de pedidos.
        /// </summary>
        /// <returns>Lista de status de pedidos.</returns>
        List<OrderStatus> GetOrderStatus();

        /// <summary>
        /// Método retorna um status de pedido.
        /// </summary>
        /// <param name="id">Identificador do status de pedido.</param>
        /// <returns>Objeto</returns>
        OrderStatus GetOrderStatus(int id);

        /// <summary>
        /// Método retorna uma lista de status do pedido.
        /// </summary>
        /// <param name="order_id">Identificador do último status do pedido.</param>
        /// <returns>Objeto</returns>
        List<OrderStatus> GetNextOrderStatus(int last_order_status_id);
    }

    public class OrderStatusRepository : IOrderStatusRepository
    {
        /// <summary>
        /// Método retorna uma lista de status de pedidos.
        /// </summary>
        /// <returns>Lista de status de pedidos.</returns>
        public List<OrderStatus> GetOrderStatus()
        {
            List<OrderStatus> order_status = new List<OrderStatus>();

            using (Entities entities = new Entities())
            {
                order_status = entities.OrderStatus.OrderBy(p => p.id).ToList();
            }

            return order_status;
        }

        /// <summary>
        /// Método retorna um status de pedido
        /// </summary>
        /// <param name="id">Identificador do status de pedido.</param>
        /// <returns>Objeto</returns>
        public OrderStatus GetOrderStatus(int id)
        {
            OrderStatus order_status = new OrderStatus();

            using (Entities entities = new Entities())
            {
                order_status = entities.OrderStatus.Where(p => p.id == id).FirstOrDefault();
            }

            return order_status;
        }

        /// <summary>
        /// Método retorna uma lista de status do pedido.
        /// </summary>
        /// <param name="order_id">Identificador do último status do pedido.</param>
        /// <returns>Objeto</returns>
        public List<OrderStatus> GetNextOrderStatus(int last_order_status_id)
        {
            List<OrderStatus> order_status = new List<OrderStatus>();

            using (Entities entities = new Entities())
            {
                order_status = entities.OrderStatus.Where(os => os.id >= last_order_status_id).OrderBy(os => os.id).ToList();
            }

            return order_status;
        }
    }
}