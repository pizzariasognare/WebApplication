using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;
using WebApplication.Components;

namespace WebApplication.Repositories
{
    public interface IOrderPizzaRepository
    {
        /// <summary>
        /// Método insere uma pizza no pedido.
        /// </summary>
        /// <param name="order_pizza">Objeto</param>
        /// <returns>Status da inserção (Verdade ou falso)</returns>
        bool Insert(OrderPizza order_pizza);

        /// <summary>
        /// Método atualiza uma pizza no pedido.
        /// </summary>
        /// <param name="order_pizza">Objeto</param>
        /// <returns>Status da atualização (Verdade ou falso)</returns>
        bool Update(OrderPizza order_pizza);

        /// <summary>
        /// Método retorna uma lista de pizzas do pedido.
        /// </summary>
        /// <param name="order_id">Identificador do pedido.</param>
        /// <returns>Lista de pizzas do pedido.</returns>
        List<OrderPizza> GetOrderPizzas(int order_id);
    }

    public class OrderPizzaRepository : IOrderPizzaRepository
    {
        private IPizzaRepository pizza_repository;

        public OrderPizzaRepository()
        {
            this.pizza_repository = new PizzaRepository();
        }

        /// <summary>
        /// Método insere um endereço de um pizza no pedido.
        /// </summary>
        /// <param name="OrderPizza">Objeto</param>
        /// <returns>Status da inserção (Verdade ou falso)</returns>
        public bool Insert(OrderPizza order_pizza)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    entities.OrderPizza.Add(order_pizza);
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
        /// Método atualiza uma pizza no pedido.
        /// </summary>
        /// <param name="OrderPizza">Objeto</param>
        /// <returns>Status da atualização (Verdade ou falso)</returns>
        public bool Update(OrderPizza order_pizza)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    entities.Entry(order_pizza).State = EntityState.Modified;
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
        /// Método retorna uma lista de pizzas do pedido.
        /// </summary>
        /// <param name="order_id">Identificador do pedido.</param>
        /// <returns>Lista de pizzas do pedido.</returns>
        public List<OrderPizza> GetOrderPizzas(int order_id)
        {
            List<OrderPizza> order_pizzas = new List<OrderPizza>();

            using (Entities entities = new Entities())
            {
                List<OrderPizza> _order_pizzas = entities.OrderPizza.Where(op => op.order_id == order_id).OrderBy(op => op.id).ToList();

                foreach (var order_pizza in _order_pizzas)
                {
                    order_pizza.Pizza = this.pizza_repository.GetPizza(order_pizza.pizza_id);
                    order_pizzas.Add(order_pizza);
                }
            }

            return order_pizzas;
        }
    }
}