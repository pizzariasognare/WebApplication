using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public interface IPizzaFlavorService
    {
        /// <summary>
        /// Método retorna uma lista de sabores de pizzas.
        /// </summary>
        /// <returns>Lista de sabores de pizzas.</returns>
        List<PizzaFlavor> GetPizzaFlavors();
    }

    public class PizzaFlavorService : IPizzaFlavorService
    {
        private IPizzaFlavorRepository pizza_flavor_repository;

        public PizzaFlavorService(IPizzaFlavorRepository pizza_flavor_repository)
        {
            this.pizza_flavor_repository = pizza_flavor_repository;
        }

        /// <summary>
        /// Método retorna uma lista de sabores de pizzas.
        /// </summary>
        /// <returns>Lista de sabores de pizzas.</returns>
        public List<PizzaFlavor> GetPizzaFlavors()
        {
            return this.pizza_flavor_repository.GetPizzaFlavors();
        }
    }
}