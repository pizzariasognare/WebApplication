using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IPizzaSizeRepository
    {
        /// <summary>
        /// Método retorna uma lista de tamanhos.
        /// </summary>
        /// <returns>Lista de tamanhos.</returns>
        List<PizzaSize> GetPizzaSizes();

        /// <summary>
        /// Método retorna um tamanho.
        /// </summary>
        /// <returns>Objeto</returns>
        PizzaSize GetPizzaSize(int id);
    }

    public class PizzaSizeRepository : IPizzaSizeRepository
    {
        /// <summary>
        /// Método retorna uma lista de tamanhos.
        /// </summary>
        /// <returns>Lista de PizzaSizes.</returns>
        public List<PizzaSize> GetPizzaSizes()
        {
            List<PizzaSize> pizza_sizes = new List<PizzaSize>();

            using (Entities entities = new Entities())
            {
                pizza_sizes = entities.PizzaSize.OrderBy(p => p.id).ToList();
            }

            return pizza_sizes;
        }      

        /// <summary>
        /// Método retorna um PizzaSize.
        /// </summary>
        /// <param name="id">Identificador da PizzaSize</param>
        /// <returns>Lista de PizzaSizes.</returns>
        public PizzaSize GetPizzaSize(int id)
        {
            PizzaSize pizza_size = new PizzaSize();

            using (Entities entities = new Entities())
            {
                pizza_size = entities.PizzaSize.Where(p => p.id == id).FirstOrDefault();
            }

            return pizza_size;
        }
    }
}