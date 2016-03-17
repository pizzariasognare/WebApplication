using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Método retorna o objeto usuário ativo por email e senha.
        /// </summary>
        /// <param name="email">E-mail</param>
        /// <param name="password">Senha</param>        
        /// <returns>Objeto usuário.</returns>
        User GetEnabledUser(string email, string password);

        /// <summary>
        /// Método retorna uma lista de usuários.
        /// </summary>
        /// <returns>Lista de usuários.</returns>
        List<User> GetUsers();

        /// <summary>
        /// Método insere um usuário.
        /// </summary>
        /// <returns>Status da inserção (Verdade ou falso).</returns>
        ReturnStatus Insert(User user);
    }

    public class UserService : IUserService
    {
        private IUserRepository user_repository;

        public UserService(IUserRepository user_repository)
        {
            this.user_repository = user_repository;
        }

        /// <summary>
        /// Método retorna o objeto usuário ativo por email e senha.
        /// </summary>
        /// <param name="email">E-mail</param>
        /// <param name="password">Senha</param>
        /// <param name="enabled">Habiltado</param>
        /// <returns>Objeto usuário.</returns>
        public User GetEnabledUser(string email, string password)
        {
            return this.user_repository.GetUser(email, password, true);
        }


        /// <summary>
        /// Método retorna uma lista de usuários.
        /// </summary>
        /// <returns>Lista de usuários.</returns>
        public List<User> GetUsers()
        {
            return this.user_repository.GetUsers();
        }

        /// <summary>
        /// Método insere um usuário.
        /// </summary>
        /// <returns>Status da inserção (Verdade ou falso).</returns>
        public ReturnStatus Insert(User user)
        {
            ReturnStatus return_status = new ReturnStatus();

            if (!this.user_repository.Insert(user))
            {
                return_status.message = "Erro ao adicionar usuário.";
                return return_status;
            }

            return_status.success = true;
            return_status.message = "Usuário cadastro com sucesso";
            return return_status;
        }
    }
}