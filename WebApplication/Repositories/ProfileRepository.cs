using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IProfileRepository 
    {
        /// <summary>
        /// Método retorna uma lista de perfis.
        /// </summary>
        /// <returns>Lista de perfis.</returns>
        List<Profile> GetProfiles();

        /// <summary>
        /// Método retorna uma lista de perfis diferente de cliente.
        /// </summary>
        /// <returns>Lista de perfis.</returns>
        List<Profile> GetNotCustomerProfiles();

        /// <summary>
        /// Método verifica se o perfil existe.
        /// </summary>
        /// <returns>Verdadeiro ou falso.</returns>
        bool Exists(int id);
    }

    public class ProfileRepository : IProfileRepository
    {
        /// <summary>
        /// Método retorna uma lista de perfis.
        /// </summary>
        /// <returns>Lista de perfis.</returns>
        public List<Profile> GetProfiles()
        {
            List<Models.Profile> profiles = new List<Profile>();

            using (Entities entities = new Entities())
            {
                profiles = entities.Profile.OrderBy(p => p.name).ToList();
            }

            return profiles;
        }

        /// <summary>
        /// Método retorna uma lista de perfis.
        /// </summary>
        /// <returns>Lista de perfis.</returns>
        public List<Profile> GetNotCustomerProfiles()
        {
            List<Models.Profile> profiles = new List<Profile>();

            using (Entities entities = new Entities())
            {
                profiles = entities.Profile.Where(p => p.id != Models.Profile.CLIENTE).OrderBy(p => p.name).ToList();
            }

            return profiles;
        }        

        /// <summary>
        /// Método verifica se o perfil existe.
        /// </summary>
        /// <returns>Verdadeiro ou falso.</returns>
        public bool Exists(int id)
        {
            bool value = false;

            using (Entities entities = new Entities())
            {
                value = (entities.Profile.Where(p => p.id == id).Count() > 0);
            }

            return value;
        }
    }
}