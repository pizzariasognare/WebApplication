using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public interface ICustomerService
    {
        /// <summary>
        /// Método retorna uma lista de clientes.
        /// </summary>
        /// <returns>Lista de clientes</returns>
        List<Customer> GetCustomers();

        /// <summary>
        /// Método retorna um cliente.
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Objeto</returns>
        Customer GetCustomer(int id);

        /// <summary>
        /// Método retorna um cliente por usuário.
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns>Objeto</returns>
        Customer GetCustomerByUserId(int user_id);

        /// <summary>
        /// Método insere um cliente.
        /// </summary>
        /// <param name="customer">Objeto cliente</param>
        /// <returns>Objeto</returns>
        ReturnStatus Insert(Customer customer);

        /// <summary>
        /// Método atualiza um cliente.
        /// </summary>
        /// <param name="customer">Objeto cliente</param>
        /// <returns>Objeto</returns>
        ReturnStatus Update(Customer customer);

        /// <summary>
        /// Método atualiza enabled de um cliente.
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <param name="value">Valor da atualização</param>
        /// <returns>Objeto</returns>
        ReturnStatus SetEnabled(int id, short value);

        /// <summary>
        /// Método retorna um cliente pelo telefone.
        /// </summary>
        /// <param name="phone">Telefone do usuário.</param>
        /// <returns>Objeto</returns>
        Customer GetCustomerByPhone(string phone);
    }

    public class CustomerService : ICustomerService
    {
        private ICustomerRepository customer_repository;

        public CustomerService(ICustomerRepository customer_repository)
        {
            this.customer_repository = customer_repository;
        }

        /// <summary>
        /// Método retorna um cliente
        /// </summary>
        /// <returns>Objeto</returns>
        public Customer GetCustomer(int id)
        {
            return this.customer_repository.GetCustomer(id);
        }

        /// <summary>
        /// Método retorna um cliente por usuário.
        /// </summary>
        /// <returns>Objeto</returns>
        public Customer GetCustomerByUserId(int user_id)
        {
            return this.customer_repository.GetCustomerByUserId(user_id);
        }

        /// <summary>
        /// Método retorna uma lista de clientes.
        /// </summary>
        /// <returns>Lista de clientes.</returns>
        public List<Customer> GetCustomers()
        {
            return this.customer_repository.GetCustomers();
        }

        /// <summary>
        /// Método insere um cliente
        /// </summary>
        /// <returns>Status da inserção (Verdade ou falso)</returns>
        public ReturnStatus Insert(Customer customer)
        {
            ReturnStatus return_status = new ReturnStatus();

            // Verifica se o telefone pertence a outro cliente.
            if (this.customer_repository.Exists(customer.phone, customer.id))
            {
                return_status.message = "Telefone já pertence a outro cliente.";
                return return_status;
            }

            // Verifica se aconteceu alguma erro no cadastro do cliente.
            int customer_id_inserted = this.customer_repository.Insert(customer);
            if (customer_id_inserted == 0)
            {
                return_status.message = "Erro ao adicionar cliente.";
            }

            return_status.success = true;
            return_status.message = "Cliente adicionado com sucesso.";
            return_status.meta.Add(Convert.ToString(customer_id_inserted));
            return return_status;
        }

        /// <summary>
        /// Método atualiza um cliente
        /// </summary>
        /// <returns>Status da atualização (Verdade ou falso)</returns>
        public ReturnStatus Update(Customer customer)
        {
            ReturnStatus return_status = new ReturnStatus();

            // Verifica se o telefone pertence a outro cliente.            
            if (this.customer_repository.Exists(customer.phone, customer.id))
            {
                return_status.message = "Telefone já pertence a outro cliente.";
                return return_status;
            }

            // Verifica se aconteceu alguma erro no cadastro do cliente.
            if (!this.customer_repository.Update(customer))
            {
                return_status.message = "Erro ao editar cliente.";
                return return_status;
            }

            return_status.success = true;
            return_status.message = "Cliente editado com sucesso.";
            return return_status;
        }

        /// <summary>
        /// Método atualiza enabled de um cliente.
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <param name="value">Valor da atualização</param>
        /// <returns>Objeto</returns>
        public ReturnStatus SetEnabled(int id, short value)
        {
            ReturnStatus return_status = new ReturnStatus();

            // Obtém o cliente.
            Customer customer = customer_repository.GetCustomer(id);

            // Verifica se o cliente existe.
            if (customer == null)
            {
                return_status.message = "Cliente inexistente";
                return return_status;
            }

            customer.enabled = value;

            // Atualiza o cliente.
            if (!customer_repository.Update(customer))
            {
                return_status.message = "Erro ao atualizar status do cliente";
                return return_status;
            }

            return_status.success = true;
            return_status.message = "Status atualizado com sucesso.";
            return return_status;
        }

        /// <summary>
        /// Método retorna um cliente pelo telefone.
        /// </summary>
        /// <param name="phone">Telefone do usuário</param>
        /// <returns>Objeto</returns>
        public Customer GetCustomerByPhone(string phone)
        {
            return this.customer_repository.GetCustomerByPhone(phone);
        }

    }
}