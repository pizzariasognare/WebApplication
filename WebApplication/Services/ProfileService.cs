using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{         
    public interface IProfileService 
    {
        /// <summary>
        /// Método retorna uma lista de perfis.
        /// </summary>
        /// <returns>Lista de perfis.</returns>
        List<Profile> GetProfiles();

        /// <summary>
        /// Método verifica se o perfil existe.
        /// </summary>
        /// <returns>Verdadeiro ou falso.</returns>
        bool Exists(int id);
    }

    public class ProfileService : IProfileService 
    {
        private IProfileRepository profile_repository;

        public ProfileService(IProfileRepository profile_repository)
        {
            this.profile_repository = profile_repository;    
        }

        /// <summary>
        /// Método retorna uma lista de perfis.
        /// </summary>
        /// <returns>Lista de perfis.</returns>
        public List<Profile> GetProfiles() 
        {
            return this.profile_repository.GetProfiles();
        }

        /// <summary>
        /// Método verifica se o perfil existe.
        /// </summary>
        /// <returns>Verdadeiro ou falso.</returns>
        public bool Exists(int id)
        {
            return this.profile_repository.Exists(id);
        }
    }
}