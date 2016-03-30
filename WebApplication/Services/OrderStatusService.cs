using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public interface IOrderStatusService
    {
        /// <summary>
        /// Método retorna uma lista de status do pedido.
        /// </summary>
        /// <param name="order_id">Identificador do último status do pedido.</param>
        /// <returns>Objeto</returns>
        List<OrderStatus> GetNextOrderStatus(int last_order_status_id);
    }

    public class OrderStatusService : IOrderStatusService
    {
        private IOrderStatusRepository order_status_repository;

        public OrderStatusService(IOrderStatusRepository order_status_repository)
        {
            this.order_status_repository = order_status_repository;
        }

        /// <summary>
        /// Método retorna uma lista de status do pedido.
        /// </summary>
        /// <param name="order_id">Identificador do último status do pedido.</param>
        /// <returns>Objeto</returns>
        public List<OrderStatus> GetNextOrderStatus(int last_order_status_id)
        {
            return this.order_status_repository.GetNextOrderStatus(last_order_status_id);
        }
    }
}