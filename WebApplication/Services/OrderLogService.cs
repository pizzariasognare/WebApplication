using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public interface IOrderLogService
    {
        /// <summary>
        /// Método insere um log do pedido.
        /// </summary>        
        /// <param name="order_log">Objeto de log do pedido</param>
        /// <returns>Objeto</returns>
        ReturnStatus Insert(OrderLog order_log);

        /// <summary>
        /// Método atualiza um log do pedido.
        /// </summary>
        /// <param name="order_log">Objeto de log do pedido</param>
        /// <returns>Objeto</returns>
        ReturnStatus Update(OrderLog order_log);
    }

    public class OrderLogService : IOrderLogService
    {
        private IOrderLogRepository order_log_repository;        

        public OrderLogService(IOrderLogRepository order_log_repository)
        {
            this.order_log_repository = order_log_repository;            
        }        

        /// <summary>
        /// Método insere um pedido.
        /// </summary>
        /// <param name="customer_id">Identificador do cliente </param>
        /// <returns>Objeto</returns>
        public ReturnStatus Insert(OrderLog order_log)
        {
            ReturnStatus return_status = new ReturnStatus();

            if (!this.order_log_repository.Insert(order_log))
            {
                return_status.message = "Erro ao adicionar log do pedido.";
                return return_status;
            }

            return_status.success = true;
            return_status.message = "Log do pedido adicionado com sucesso.";
            return return_status;
        }

        /// <summary>
        /// Método atualiza um log do pedido.
        /// </summary>
        /// <param name="order_log">Objeto de log do pedido</param>
        /// <returns>Objeto</returns>
        public ReturnStatus Update(OrderLog order_log)
        {
            ReturnStatus return_status = new ReturnStatus();

            if (!this.order_log_repository.Update(order_log))
            {
                return_status.message = "Erro ao adicionar log do pedido.";
                return return_status;
            }

            return_status.success = true;
            return_status.message = "Log do pedido adicionado com sucesso.";
            return return_status;
        }
    }
}