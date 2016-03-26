using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public interface IOrderPizzaService
    {
        /// <summary>
        /// Método insere uma pizza do pedido.
        /// </summary>        
        /// <param name="order_pizza">Objeto de pizza do pedido</param>
        /// <returns>Objeto</returns>
        ReturnStatus Insert(OrderPizza order_pizza);

        /// <summary>
        /// Método atualiza uma pizza do pedido.
        /// </summary>
        /// <param name="order_pizza">Objeto de pizza do pedido</param>
        /// <returns>Objeto</returns>
        ReturnStatus Update(OrderPizza order_pizza);
    }

    public class OrderPizzaService : IOrderPizzaService
    {
        private IOrderPizzaRepository order_pizza_repository;        

        public OrderPizzaService(IOrderPizzaRepository order_pizza_repository)
        {
            this.order_pizza_repository = order_pizza_repository;            
        }

        /// <summary>
        /// Método insere uma pizza do pedido.
        /// </summary>        
        /// <param name="order_pizza">Objeto de pizza do pedido</param>
        /// <returns>Objeto</returns>
        public ReturnStatus Insert(OrderPizza order_pizza)
        {
            ReturnStatus return_status = new ReturnStatus();

            if (!this.order_pizza_repository.Insert(order_pizza))
            {
                return_status.message = "Erro ao adicionar pizza do pedido.";
                return return_status;
            }

            return_status.success = true;
            return_status.message = "pizza do pedido adicionado com sucesso.";
            return return_status;
        }

        /// <summary>
        /// Método atualiza uma pizza do pedido.
        /// </summary>
        /// <param name="order_pizza">Objeto de pizza do pedido</param>
        /// <returns>Objeto</returns>
        public ReturnStatus Update(OrderPizza order_pizza)
        {
            ReturnStatus return_status = new ReturnStatus();

            if (!this.order_pizza_repository.Update(order_pizza))
            {
                return_status.message = "Erro ao adicionar pizza do pedido.";
                return return_status;
            }

            return_status.success = true;
            return_status.message = "pizza do pedido adicionado com sucesso.";
            return return_status;
        }
    }
}