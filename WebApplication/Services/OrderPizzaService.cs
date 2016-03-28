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
        /// Método Remove uma pizza do pedido.
        /// </summary>
        /// <param name="order_pizza">Objeto de pizza do pedido</param>
        /// <returns>Objeto</returns>
        ReturnStatus Delete(OrderPizza order_pizza);

        /// <summary>
        /// Método retorna uma pizza do pedido.
        /// </summary>
        /// <param name="order_id">Identificador da pizza no pedido.</param>
        /// <returns>Objeto</returns>
        OrderPizza GetOrderPizza(int order_pizza_id);
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
                return_status.message = "Erro ao adicionar pizza ao pedido.";
                return return_status;
            }            
            
            return_status.success = true;
            return_status.message = "Adicionado pizzao ao pedido com sucesso.";
            return return_status;
        }

        /// <summary>
        /// Método atualiza uma pizza do pedido.
        /// </summary>
        /// <param name="order_pizza">Objeto de pizza do pedido</param>
        /// <returns>Objeto</returns>
        public ReturnStatus Delete(OrderPizza order_pizza)
        {
            ReturnStatus return_status = new ReturnStatus();

            if (!this.order_pizza_repository.Delete(order_pizza))
            {
                return_status.message = "Erro ao remover pizza do pedido.";
                return return_status;
            }

            return_status.success = true;
            return_status.message = "pizza do pedido removido com sucesso.";
            return return_status;
        }

        /// <summary>
        /// Método retorna uma pizza do pedido.
        /// </summary>
        /// <param name="order_id">Identificador da pizza no pedido.</param>
        /// <returns>Objeto</returns>
        public OrderPizza GetOrderPizza(int order_pizza_id)
        {
            return this.order_pizza_repository.GetOrderPizza(order_pizza_id);
        }
    }
}