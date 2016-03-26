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
                List<Pizza> _pizzas = entities.Pizza.OrderBy(p => p.id).ToList();

                foreach (var _pizza in _pizzas)
                {
                    _pizza.PizzaFlavor = pizza_flavor_repository.GetPizzaFlavor(_pizza.pizza_flavor_id);
                    _pizza.PizzaSize = pizza_size_repository.GetPizzaSize(_pizza.pizza_size_id);
                    pizzas.Add(_pizza);
                }
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

                if (pizza != null)
                {
                    pizza.PizzaFlavor = pizza_flavor_repository.GetPizzaFlavor(pizza.pizza_flavor_id);
                    pizza.PizzaSize = pizza_size_repository.GetPizzaSize(pizza.pizza_size_id);                    
                }
            }

            return pizza;
        }
    }
}