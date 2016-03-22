using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;
using WebApplication.Components;

namespace WebApplication.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Método retorna o objeto usuário por email e senha.
        /// </summary>
        /// <param name="email">E-mail</param>
        /// <param name="password">Senha</param>
        /// <param name="enabled">Habiltado</param>
        /// <returns>Objeto usuário.</returns>
        User GetUser(string email, string password, bool enabled);

        /// <summary>
        /// Método retorna o objeto usuário por id.
        /// </summary>
        /// <param name="email">Identificador do usuário.</param>        
        /// <returns>Objeto usuário.</returns>
        User GetUser(int id);

        /// <summary>
        /// Método retorna o objeto usuário por email.
        /// </summary>
        /// <param name="email">Identificador do usuário.</param>        
        /// <returns>Objeto usuário.</returns>
        User GetUser(string email);

        /// <summary>
        /// Método retorna uma lista de usuários.
        /// </summary>
        /// <returns>Lista de usuários.</returns>
        List<User> GetUsers();

        /// <summary>
        /// Método insere um usuário.
        /// </summary>
        /// <returns>Status da inserção (Verdade ou falso).</returns>
        bool Insert(User user);

        /// <summary>
        /// Método atualiza um usuário.
        /// </summary>
        ///  <param name="user">Objeto</param>
        /// <returns>Status da atualização (Verdade ou falso).</returns>
        bool Update(User user);
    }
    public class UserRepository : IUserRepository
    {
        private ProfileRepository profile_repository;

        public UserRepository()
        { 
            this.profile_repository = new ProfileRepository();
        }


        /// <summary>
        /// Método retorna o objeto usuário por email e senha.
        /// </summary>
        /// <param name="email">E-mail</param>
        /// <param name="password">Senha</param>
        /// <returns>Objeto usuário.</returns>
        public User GetUser(string email, string password, bool enabled)
        {
            password = Cryptography.ConvertToMD5(password);

            User user = new User();
            using (Entities entities = new Entities())
            {
                user = entities.User.Where(u => (u.email == email && u.password == password && u.enabled == (enabled ? 1 : 0))).FirstOrDefault();

                if (user != null)
                {
                    user.Profile = this.profile_repository.GetProfile(user.profile_id);
                }
            }

            return user;
        }

        /// <summary>
        /// Método retorna o objeto usuário por email.
        /// </summary>
        /// <param name="id">Identificador do usuário.<param>
        /// <returns>Objeto usuário.</returns>
        public User GetUser(string email)
        {
            User user = new User();
            using (Entities entities = new Entities())
            {
                user = entities.User.Where(u => u.email == email).FirstOrDefault();

                if (user != null)
                {
                    user.Profile = this.profile_repository.GetProfile(user.profile_id);
                }
            }

            return user;
        }

        /// <summary>
        /// Método retorna o objeto usuário por id.
        /// </summary>
        /// <param name="id">Identificador do usuário.<param>
        /// <returns>Objeto usuário.</returns>
        public User GetUser(int id)
        {
            User user = new User();
            using (Entities entities = new Entities())
            {
                user = entities.User.Where(u => u.id == id).FirstOrDefault();

                if (user != null)
                {
                    user.Profile = this.profile_repository.GetProfile(user.profile_id);
                }
            }

            return user;
        }

        /// <summary>
        /// Método retorna uma lista de usuários.
        /// </summary>
        /// <returns>Lista de usuários.</returns>
        public List<User> GetUsers()
        {
            List<Models.User> Users = new List<User>();

            using (Entities entities = new Entities())
            {
                Users = entities.User.OrderBy(u => u.email).ToList();
            }

            return Users;
        }

        /// <summary>
        /// Método insere um usuário.
        /// </summary>
        /// <returns>Status da inserção (Verdade ou falso).</returns>
        public bool Insert(User user)
        {
            try
            {
                user.password = Cryptography.ConvertToMD5(user.password);
                user.enabled = 1;

                using (Entities entities = new Entities())
                {
                    entities.User.Add(user);
                    entities.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Método atualiza um usuário.
        /// </summary>
        /// <returns>Status da atualização (Verdade ou falso).</returns>
        public bool Update(User user)
        {
            try
            {
                User _user = this.GetUser(user.id);
                if (_user.password != user.password)
                {
                    user.password = Cryptography.ConvertToMD5(user.password);
                }

                using (Entities entities = new Entities())
                {
                    entities.Entry(user).State = EntityState.Modified;
                    entities.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}