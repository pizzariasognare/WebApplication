using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public interface IPizzaService
    {
        /// <summary>
        /// Método retorna uma lista de pizzas.
        /// </summary>
        /// <returns>Lista de pizzas.</returns>
        List<Pizza> GetPizzas();
    }

    public class PizzaService : IPizzaService
    {
        private IPizzaRepository pizza_repository;

        public PizzaService(IPizzaRepository pizza_repository)
        {
            this.pizza_repository = pizza_repository;
        }

        /// <summary>
        /// Método retorna uma lista de pizzas.
        /// </summary>
        /// <returns>Lista de pizzas.</returns>
        public List<Pizza> GetPizzas()
        {
            return this.pizza_repository.GetPizzas();
        }
    }
}