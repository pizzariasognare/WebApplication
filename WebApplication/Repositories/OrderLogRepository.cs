using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IOrderLogRepository
    {
        /// <summary>
        /// Método retorna uma lista logs do pedido.
        /// </summary>
        /// <param name="order_id">Identificador do pedido</param>
        /// <returns>Lista de logs do pedido.</returns>
        List<OrderLog> GetOrderLogs(int order_id);

        /// <summary>
        /// Método retorna um log do pedido.
        /// </summary>
        /// <param name="id">Identificador do log do pedido.</param>
        /// <returns>Objeto</returns>
        OrderLog GetOrderLog(int id);

        /// <summary>
        /// Método retorna o último log do pedido.
        /// </summary>
        /// <param name="id">Identificador do log do pedido.</param>
        /// <returns>Objeto</returns>
        OrderLog GetLastOrderLog(int order_id);

        /// <summary>
        /// Método insere um log no pedido.
        /// </summary>
        /// <param name="order_log">Objeto</param>
        /// <returns>Status da inserção (Verdade ou falso)</returns>
        bool Insert(OrderLog order_log);

        /// <summary>
        /// Método atualiza um no pedido.
        /// </summary>
        /// <param name="order_log">Objeto</param>
        /// <returns>Status da atualização (Verdade ou falso)</returns>
        bool Update(OrderLog order_log);
    }

    public class OrderLogRepository : IOrderLogRepository
    {
        public IOrderStatusRepository order_status_repository;
        public IUserRepository user_repository;

        public OrderLogRepository()
        {
            this.order_status_repository = new OrderStatusRepository();
            this.user_repository = new UserRepository();
        }

        /// <summary>
        /// Método retorna uma lista logs do pedido.
        /// </summary>
        /// <param name="order_id">Identificador do pedido</param>
        /// <returns>Lista de logs dos pedidos.</returns>
        public List<OrderLog> GetOrderLogs(int order_id)
        {
            List<OrderLog> order_logs = new List<OrderLog>();

            using (Entities entities = new Entities())
            {
                List<OrderLog> _order_logs = entities.OrderLog.OrderBy(o => o.order_id == order_id).ToList();

                foreach (var order_log in order_logs)
                {
                    order_logs.Add(this.GetOrderLog(order_log.id));
                }
            }

            return order_logs;
        }

        /// <summary>
        /// Método retorna um pedido.
        /// </summary>
        /// <param name="id">Identificador do pedido.</param>
        /// <returns>Objeto</returns>
        public OrderLog GetOrderLog(int id)
        {
            OrderLog order_log = new OrderLog();

            using (Entities entities = new Entities())
            {
                order_log = entities.OrderLog.Where(o => o.id == id).FirstOrDefault();

                if (order_log != null)
                {
                    order_log.OrderStatus = this.order_status_repository.GetOrderStatus(order_log.order_status_id);
                    order_log.User = this.user_repository.GetUser(order_log.user_id);
                }
            }

            return order_log;
        }

        /// <summary>
        /// Método retorna o último log do pedido.
        /// </summary>
        /// <param name="id">Identificador do log do pedido.</param>
        /// <returns>Objeto</returns>
        public OrderLog GetLastOrderLog(int order_id)
        {
            OrderLog order_log = new OrderLog();

            using (Entities entities = new Entities())
            {
                order_log = entities.OrderLog.Where(o => o.order_id == order_id).OrderByDescending(o => o.order_log_datetime).FirstOrDefault();

                if (order_log != null)
                {
                    order_log.OrderStatus = this.order_status_repository.GetOrderStatus(order_log.order_status_id);
                    order_log.User = this.user_repository.GetUser(order_log.user_id);
                }
            }

            return order_log;
        }

        /// <summary>
        /// Método insere um log no pedido.
        /// </summary>
        /// <param name="order_log">Objeto</param>
        /// <returns>Status da inserção (Verdade ou falso)</returns>
        public bool Insert(OrderLog order_log)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    entities.OrderLog.Add(order_log);
                    entities.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Método atualiza um log no pedido.
        /// </summary>
        /// <param name="order_log">Objeto</param>
        /// <returns>Status da inserção (Verdade ou falso)</returns>
        public bool Update(OrderLog order_log)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    entities.Entry(order_log).State = EntityState.Modified;
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