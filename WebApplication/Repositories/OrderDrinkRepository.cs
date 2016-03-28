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
        /// Método insere uma bebida no pedido.
        /// </summary>
        /// <param name="order_drink">Objeto</param>
        /// <returns>Status da inserção./returns>
        bool Insert(OrderDrink order_drink);

        /// <summary>
        /// Método remove uma bebida no pedido.
        /// </summary>
        /// <param name="OrderDrink">Objeto</param>
        /// <returns>Status da remoção (Verdade ou falso)</returns>
        bool Delete(OrderDrink order_drink);

        /// <summary>
        /// Método retorna uma lista de bebidas do pedido.
        /// </summary>
        /// <param name="order_id">Identificador do pedido.</param>
        /// <returns>Lista de pizzas do pedido.</returns>
        List<OrderDrink> GetOrderDrinks(int order_id);

        /// <summary>
        /// Método retorna uma pizza do pedido.
        /// </summary>
        /// <param name="order_id">Identificador da pizza no pedido.</param>
        /// <returns>Objeto</returns>
        OrderDrink GetOrderDrink(int order_drink_id);
    }

    public class OrderDrinkRepository : IOrderDrinkRepository
    {
        private IDrinkRepository drink_repository;

        public OrderDrinkRepository()
        {
            this.drink_repository = new DrinkRepository();
        }

        /// <summary>
        /// Método insere uma bebida no pedido.
        /// </summary>
        /// <param name="order_drink">Objeto</param>
        /// <returns>Status da inserção./returns>
        public bool Insert(OrderDrink order_drink)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    Drink drink = entities.Drink.Where(d => d.id == order_drink.drink_id).FirstOrDefault();
                    Order order = entities.Order.Where(o => o.id == order_drink.order_id).FirstOrDefault();
                    order.price += drink.price;
                    order.final_price += drink.price;

                    entities.Entry(order).State = EntityState.Modified;
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
        /// Método remove uma bebida no pedido.
        /// </summary>
        /// <param name="order_drink">Objeto</param>
        /// <returns>Status da remoção (Verdade ou falso)</returns>
        public bool Delete(OrderDrink order_drink)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    Drink drink = entities.Drink.Where(d => d.id == order_drink.drink_id).FirstOrDefault();
                    Order order = entities.Order.Where(o => o.id == order_drink.order_id).FirstOrDefault();
                    order.price -= drink.price;
                    order.final_price -= drink.price;

                    entities.Entry(order).State = EntityState.Modified;
                    entities.Entry(order_drink).State = EntityState.Deleted;
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
        /// Método retorna uma lista de bebidas do pedido.
        /// </summary>
        /// <param name="order_id">Identificador do pedido.</param>
        /// <returns>Lista de bebidas do pedido.</returns>
        public List<OrderDrink> GetOrderDrinks(int order_id)
        {

            List<OrderDrink> order_drinks = new List<OrderDrink>();

            using (Entities entities = new Entities())
            {
                List<OrderDrink> _order_drinks = entities.OrderDrink.Where(op => op.order_id == order_id).OrderBy(op => op.id).ToList();

                foreach (var order_drink in _order_drinks)
                {
                    order_drink.Drink = this.drink_repository.GetDrink(order_drink.drink_id);
                    order_drinks.Add(order_drink);
                }
            }

            return order_drinks;
        }

        /// <summary>
        /// Método retorna uma bebida do pedido.
        /// </summary>
        /// <param name="order_id">Identificador da bebida no pedido.</param>
        /// <returns>Objeto</returns>
        public OrderDrink GetOrderDrink(int order_drink_id)
        {
            OrderDrink order_drink = new OrderDrink();

            using (Entities entities = new Entities())
            {
                order_drink = entities.OrderDrink.Where(od => od.id == order_drink_id).FirstOrDefault();
            }

            return order_drink;
        }
    }
}