using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public interface IPizzaSizeService
    {
        /// <summary>
        /// Método retorna uma lista de tamanhos de pizzas.
        /// </summary>
        /// <returns>Lista de tamanhos de pizzas.</returns>
        List<PizzaSize> GetPizzaSizes();
    }

    public class PizzaSizeService : IPizzaSizeService
    {
        private IPizzaSizeRepository pizza_flavor_repository;

        public PizzaSizeService(IPizzaSizeRepository pizza_flavor_repository)
        {
            this.pizza_flavor_repository = pizza_flavor_repository;
        }

        /// <summary>
        /// Método retorna uma lista de tamanhos de pizzas.
        /// </summary>
        /// <returns>Lista de tamanhos de pizzas.</returns>
        public List<PizzaSize> GetPizzaSizes()
        {
            return this.pizza_flavor_repository.GetPizzaSizes();
        }
    }
}