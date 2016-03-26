using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;
using WebApplication.Components;

namespace WebApplication.Repositories
{
    public interface IOrderDrinkRepository
    {
        /// <summary>
        /// Método insere um endereço de uma drink no pedido.
        /// </summary>
        /// <param name="order_drink">Objeto</param>
        /// <returns>Status da inserção (Verdade ou falso)</returns>
        bool Insert(OrderDrink order_drink);

        /// <summary>
        /// Método atualiza uma drink no pedido.
        /// </summary>
        /// <param name="order_drink">Objeto</param>
        /// <returns>Status da atualização (Verdade ou falso)</returns>
        bool Update(OrderDrink order_drink);

        /// <summary>
        /// Método retorna uma lista de drinks do pedido.
        /// </summary>
        /// <param name="order_id">Identificador do pedido.</param>
        /// <returns>Lista de drinks do pedido.</returns>
        List<OrderDrink> GetOrderDrinks(int order_id);
    }

    public class OrderDrinkRepository : IOrderDrinkRepository
    {
        private IDrinkRepository drink_repository;

        public OrderDrinkRepository()
        {
            this.drink_repository = new DrinkRepository();
        }

        /// <summary>
        /// Método insere um endereço de um drink no pedido.
        /// </summary>
        /// <param name="OrderDrink">Objeto</param>
        /// <returns>Status da inserção (Verdade ou falso)</returns>
        public bool Insert(OrderDrink order_drink)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    entities.OrderDrink.Add(order_drink);
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
        /// Método atualiza uma drink no pedido.
        /// </summary>
        /// <param name="OrderDrink">Objeto</param>
        /// <returns>Status da atualização (Verdade ou falso)</returns>
        public bool Update(OrderDrink order_drink)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    entities.Entry(order_drink).State = EntityState.Modified;
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
        /// Método retorna uma lista de drinks do pedido.
        /// </summary>
        /// <param name="order_id">Identificador do pedido.</param>
        /// <returns>Lista de drinks do pedido.</returns>
        public List<OrderDrink> GetOrderDrinks(int order_id)
        {
            List<OrderDrink> order_drinks = new List<OrderDrink>();

            using (Entities entities = new Entities())
            {
                List<OrderDrink> _order_drinks = entities.OrderDrink.Where(od => od.order_id == order_id).OrderBy(od => od.id).ToList();

                foreach (var order_drink in _order_drinks)
                {
                    order_drink.Drink = this.drink_repository.GetDrink(order_drink.drink_id);
                    order_drinks.Add(order_drink);
                }
            }

            return order_drinks;
        }
    }
}