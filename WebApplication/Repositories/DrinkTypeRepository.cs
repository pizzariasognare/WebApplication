using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IDrinkTypeRepository
    {
        /// <summary>
        /// Método retorna uma lista de tipos de bebidas.
        /// </summary>
        /// <returns>Lista de tipos de bebidas.</returns>
        List<DrinkType> GetDrinkTypes();

        /// <summary>
        /// Método retorna um tipo de bebida.
        /// </summary>
        /// <param name="id">Identificador do tipo de bebida.</param>
        /// <returns>Objeto</returns>
        DrinkType GetDrinkType(int id);
    }

    public class DrinkTypeRepository : IDrinkTypeRepository
    {
        /// <summary>
        /// Método retorna uma lista de tipos de bebidas.
        /// </summary>
        /// <returns>Lista de tipos de bebidas.</returns>
        public List<DrinkType> GetDrinkTypes()
        {
            List<DrinkType> drink_types = new List<DrinkType>();

            using (Entities entities = new Entities())
            {
                drink_types = entities.DrinkType.OrderBy(p => p.id).ToList();
            }

            return drink_types;
        }

        /// <summary>
        /// Método retorna um tipo de bebida
        /// </summary>
        /// <param name="id">Identificador do tipo de bebida.</param>
        /// <returns>Objeto</returns>
        public DrinkType GetDrinkType(int id)
        {
            DrinkType drink_type = new DrinkType();

            using (Entities entities = new Entities())
            {
                drink_type = entities.DrinkType.Where(p => p.id == id).FirstOrDefault();
            }

            return drink_type;
        }
    }
}