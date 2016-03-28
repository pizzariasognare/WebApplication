﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IDrinkRepository
    {
        /// <summary>
        /// Método retorna uma lista de bebidas.
        /// </summary>
        /// <returns>Lista de bebidas.</returns>
        List<Drink> GetDrinks();

        /// <summary>
        /// Método retorna uma bebida.
        /// </summary>
        /// <param name="id">Identificador de bebida.</param>
        /// <returns>Objeto</returns>
        Drink GetDrink(int id);
    }

    public class DrinkRepository : IDrinkRepository
    {
        private IDrinkTypeRepository drink_type_repository;

        public DrinkRepository()
        {
            this.drink_type_repository = new DrinkTypeRepository();
        }

        /// <summary>
        /// Método retorna uma lista de bebidas.
        /// </summary>
        /// <returns>Lista de bebidas.</returns>
        public List<Drink> GetDrinks()
        {
            List<Drink> drinks = new List<Drink>();

            using (Entities entities = new Entities())
            {
                drinks = entities.Drink.OrderBy(d => d.id).ToList();

                foreach (var drink in drinks)
                {
                    drink.DrinkType = this.drink_type_repository.GetDrinkType(drink.drink_type_id);
                }
            }

            return drinks;
        }

        /// <summary>
        /// Método retorna uma bebida.
        /// </summary>
        /// <param name="id">Identificador da bebida.</param>
        /// <returns>Objeto</returns>
        public Drink GetDrink(int id)
        {
            Drink drink = new Drink();

            using (Entities entities = new Entities())
            {
                drink = entities.Drink.Where(d => d.id == id).FirstOrDefault();
            }

            return drink;
        }
    }
}