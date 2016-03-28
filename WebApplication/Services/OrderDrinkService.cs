using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public interface IOrderDrinkService
    {
        /// <summary>
        /// Método insere uma bebida do pedido.
        /// </summary>        
        /// <param name="order_drink">Objeto de bebida do pedido</param>
        /// <returns>Objeto</returns>
        ReturnStatus Insert(OrderDrink order_drink);

        /// <summary>
        /// Método remove uma bebida do pedido.
        /// </summary>
        /// <param name="order_drink">Objeto de bebida do pedido</param>
        /// <returns>Objeto</returns>
        ReturnStatus Delete(OrderDrink order_drink);

        /// <summary>
        /// Método retorna uma bebida do pedido.
        /// </summary>
        /// <param name="order_id">Identificador da bebida no pedido.</param>
        /// <returns>Objeto</returns>
        OrderDrink GetOrderDrink(int order_drink_id);
    }

    public class OrderDrinkService : IOrderDrinkService
    {
        private IOrderDrinkRepository order_drink_repository;

        public OrderDrinkService(IOrderDrinkRepository order_drink_repository)
        {
            this.order_drink_repository = order_drink_repository;
        }

        /// <summary>
        /// Método insere uma Drink do pedido.
        /// </summary>        
        /// <param name="order_drink">Objeto de Drink do pedido</param>
        /// <returns>Objeto</returns>
        public ReturnStatus Insert(OrderDrink order_drink)
        {
            ReturnStatus return_status = new ReturnStatus();

            if (!this.order_drink_repository.Insert(order_drink))
            {
                return_status.message = "Erro ao adicionar bebida ao pedido.";
                return return_status;
            }

            return_status.success = true;
            return_status.message = "Adicionado bebida ao pedido com sucesso.";
            return return_status;
        }

        /// <summary>
        /// Método atualiza uma Drink do pedido.
        /// </summary>
        /// <param name="order_drink">Objeto de Drink do pedido</param>
        /// <returns>Objeto</returns>
        public ReturnStatus Delete(OrderDrink order_drink)
        {
            ReturnStatus return_status = new ReturnStatus();

            if (!this.order_drink_repository.Delete(order_drink))
            {
                return_status.message = "Erro ao remover bebida do pedido.";
                return return_status;
            }

            return_status.success = true;
            return_status.message = "Bebida do pedido removido com sucesso.";
            return return_status;
        }

        /// <summary>
        /// Método retorna uma bebida do pedido.
        /// </summary>
        /// <param name="order_id">Identificador da bebida no pedido.</param>
        /// <returns>Objeto</returns>
        public OrderDrink GetOrderDrink(int order_drink_id)
        {
            return this.order_drink_repository.GetOrderDrink(order_drink_id);
        }
    }
}