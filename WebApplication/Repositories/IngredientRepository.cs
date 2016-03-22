using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IIngredientRepository
    {
        /// <summary>
        /// Método retorna uma lista de ingredientes.
        /// </summary>
        /// <returns>Lista de ingredientes.</returns>
        List<Ingredient> GetIngredients();

        /// <summary>
        /// Método retorna um ingrediente.
        /// </summary>
        /// <returns>Objeto</returns>
        Ingredient GetIngredient(int id);
    }

    public class IngredientRepository : IIngredientRepository
    {       
        /// <summary>
        /// Método retorna uma lista de ingredientes.
        /// </summary>
        /// <returns>Lista de ingredientes.</returns>
        public List<Ingredient> GetIngredients()
        {
            List<Ingredient> ingredients = new List<Ingredient>();

            using (Entities entities = new Entities())
            {
                ingredients = entities.Ingredient.OrderBy(p => p.id).ToList();
            }

            return ingredients;
        }

        /// <summary>
        /// Método retorna um ingrediente.
        /// </summary>
        /// <param name="id">Identificador da ingrediente</param>
        /// <returns>Lista de ingredientes.</returns>
        public Ingredient GetIngredient(int id)
        {
            Ingredient ingredient = new Ingredient();

            using (Entities entities = new Entities())
            {
                ingredient = entities.Ingredient.Where(p => p.id == id).FirstOrDefault();
            }

            return ingredient;
        }
    }
}