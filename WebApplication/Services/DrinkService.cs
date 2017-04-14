using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public interface IDrinkService
    {        
        /// <summary>
        /// Método retorna uma lista de bebidas.
        /// </summary>
        /// <returns>Lista de bebidas.</returns>
        List<Drink> GetDrinks(bool? enabled);
    }

    public class DrinkService : IDrinkService
    {
        private IDrinkRepository drink_repository;

        public DrinkService(IDrinkRepository drink_repository)
        {
            this.drink_repository = drink_repository;
        }        

        /// <summary>
        /// Método retorna uma lista de bebidas.
        /// </summary>
        /// <returns>Lista de bebidas.</returns>
        public List<Drink> GetDrinks(bool? enabled)
        {
            return this.drink_repository.GetDrinks(enabled);
        }
    }
}