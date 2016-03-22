using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IPizzaRepository
    {
        /// <summary>
        /// Método retorna uma lista de pizzas.
        /// </summary>
        /// <returns>Lista de pizzas.</returns>
        List<Pizza> GetPizzas();

        /// <summary>
        /// Método retorna um pizza
        /// </summary>
        /// <returns>Objeto</returns>
        Pizza GetPizza(int id);
    }

    public class PizzaRepository : IPizzaRepository
    {
        IPizzaSizeRepository pizza_size_repository;
        IPizzaFlavorRepository pizza_flavor_repository;

        public PizzaRepository()
        {
            this.pizza_size_repository = new PizzaSizeRepository();
            this.pizza_flavor_repository = new PizzaFlavorRepository();
        }

        /// <summary>
        /// Método retorna uma lista de pizzas.
        /// </summary>
        /// <returns>Lista de pizzas.</returns>
        public List<Pizza> GetPizzas()
        {
            List<Pizza> pizzas = new List<Pizza>();            

            using (Entities entities = new Entities())
            {

                pizzas = entities.Pizza.OrderBy(p => p.id).ToList();              
            }

            return pizzas;
        }

        /// <summary>
        /// Método retorna um pizza.
        /// </summary>
        /// <param name="id">Identificador da pizza</param>
        /// <returns>Lista de pizzas.</returns>
        public Pizza GetPizza(int id)
        {
            Pizza pizza = new Pizza();

            using (Entities entities = new Entities())
            {
                pizza = entities.Pizza.Where(p => p.id == id).FirstOrDefault();
            }

            return pizza;
        }
    }
}