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
        /// Método insere um usuário para o cliente.
        /// </summary>
        /// <returns>Status da inserção (Verdade ou falso).</returns>
        ReturnStatus Insert(User user, Customer customer);

        /// <summary>
        /// Método atualiza um usuário.
        /// </summary>
        /// <returns>Status da atualização (Verdade ou falso).</returns>
        ReturnStatus Update(User user);
    }

    public class UserService : IUserService
    {
        private IUserRepository user_repository;
        private ICustomerService customer_service;

        public UserService(IUserRepository user_repository)
        {
            this.user_repository = user_repository;
            this.customer_service = new CustomerService(new CustomerRepository());
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
        /// Método insere um usuário para um cliente.
        /// </summary>
        /// <returns>Status da inserção (Verdade ou falso).</returns>
        public ReturnStatus Insert(User user, Customer customer)
        {
            ReturnStatus return_status = new ReturnStatus();

            // Verifica se email já existe.
            User _user = this.user_repository.GetUser(user.email);
            if (_user != null)
            {
                return_status.message = "E-mail já pertence a outro usuário.";
                return return_status;
            }

            // Insere o usuário.
            if (this.user_repository.Insert(user))
            {
                // Vincula usuário a cliente.
                _user = this.user_repository.GetUser(user.email);
                customer.user_id = _user.id;

                return_status = this.customer_service.Update(customer);
                if (!return_status.success)
                {
                    return return_status;
                }
            }
            else
            {
                return_status.message = "Erro ao adicionar usuário.";
                return return_status;
            }

            return_status.success = true;
            return_status.message = "Usuário cadastro com sucesso";
            return return_status;
        }

        /// <summary>
        /// Método insere um usuário.
        /// </summary>
        /// <returns>Status da inserção (Verdade ou falso).</returns>
        public ReturnStatus Update(User user)
        {
            ReturnStatus return_status = new ReturnStatus();

            if (!this.user_repository.Update(user))
            {
                return_status.message = "Erro ao atualizar usuário.";
                return return_status;
            }

            return_status.success = true;
            return_status.message = "Usuário atualizado com sucesso";
            return return_status;
        }
    }
}