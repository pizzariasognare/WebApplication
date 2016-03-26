using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IPizzaFlavorRepository
    {
        /// <summary>
        /// Método retorna uma lista de sabores.
        /// </summary>
        /// <returns>Lista de sabores.</returns>
        List<PizzaFlavor> GetPizzaFlavors();

        /// <summary>
        /// Método retorna um sabor.
        /// </summary>
        /// <param name="id">Identificador do sabor</param>
        /// <returns>Objeto</returns>
        PizzaFlavor GetPizzaFlavor(int id);
    }

    public class PizzaFlavorRepository : IPizzaFlavorRepository
    {
        IIngredientRepository ingredient_repository;

        public PizzaFlavorRepository()
        {
            this.ingredient_repository = new IngredientRepository();
        }

        /// <summary>
        /// Método retorna uma lista de sabores.
        /// </summary>
        /// <returns>Lista de sabores.</returns>
        public List<PizzaFlavor> GetPizzaFlavors()
        {
            List<PizzaFlavor> pizza_flavors = new List<PizzaFlavor>();

            using (Entities entities = new Entities())
            {
                List<PizzaFlavor> _pizza_flavors = entities.PizzaFlavor.OrderBy(p => p.id).ToList();

                foreach (var _pizza_flavor in _pizza_flavors)
                {
                    List<PizzaFlavorIngredient> pizza_flavor_ingredients = entities.PizzaFlavorIngredient.Where(psi => psi.pizza_flavor_id == _pizza_flavor.id).ToList();
                    foreach (var pizza_flavor_ingredient in pizza_flavor_ingredients)
                    {
                        _pizza_flavor.Ingredient.Add(this.ingredient_repository.GetIngredient(pizza_flavor_ingredient.ingredient_id));
                    }

                    _pizza_flavor.Pizza = entities.Pizza.Where(p => p.pizza_flavor_id == _pizza_flavor.id).ToList();

                    pizza_flavors.Add(_pizza_flavor);
                }
            }

            return pizza_flavors;
        }

        /// <summary>
        /// Método retorna um sabor
        /// </summary>
        /// <param name="id">Identificador do sabor</param>
        /// <returns>Lista de sabores.</returns>
        public PizzaFlavor GetPizzaFlavor(int id)
        {
            PizzaFlavor pizza_flavor = new PizzaFlavor();

            using (Entities entities = new Entities())
            {
                pizza_flavor = entities.PizzaFlavor.Where(p => p.id == id).FirstOrDefault();

                if (pizza_flavor != null)
                {
                    List<PizzaFlavorIngredient> pizza_flavor_ingredients = entities.PizzaFlavorIngredient.Where(psi => psi.pizza_flavor_id == pizza_flavor.id).ToList();
                    foreach (var pizza_flavor_ingredient in pizza_flavor_ingredients)
                    {
                        pizza_flavor.Ingredient.Add(this.ingredient_repository.GetIngredient(pizza_flavor_ingredient.ingredient_id));
                    }

                    pizza_flavor.Pizza = entities.Pizza.Where(p => p.pizza_flavor_id == pizza_flavor.id).ToList();
                }
            }

            return pizza_flavor;
        }
    }
}