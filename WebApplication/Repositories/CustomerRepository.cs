using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;
using WebApplication.Components;

namespace WebApplication.Repositories
{
    public interface ICustomerRepository
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
        /// <param name="user_id">Identificador do usuário</param>
        /// <returns>Objeto</returns>
        Customer GetCustomerByUserId(int user_id);

        /// <summary>
        /// Método verifica se existe algum cliente com o telefone.
        /// </summary>
        /// <param name="phone">Telefone do cliente</param>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Objeto</returns>
        bool Exists(string phone, int id);

        /// <summary>
        /// Método insere um cliente.
        /// </summary>
        /// <param name="customer">Objeto cliente</param>
        /// <returns>Status da inserção (Verdade ou falso)</returns>
        int Insert(Customer customer);

        /// <summary>
        /// Método atualiza um cliente.
        /// </summary>
        /// <param name="customer">Objeto cliente</param>
        /// <returns>Status da atualização (Verdade ou falso)</returns>
        bool Update(Customer customer);

        /// <summary>
        /// Método retorna um cliente por telefone.
        /// </summary>
        /// <param name="phone">Phone</param>
        /// <returns>Objeto</returns>
        Customer GetCustomerByPhone(string phone);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private ICustomerAddressRepository customer_address_repository;
        private IUserRepository user_repository;

        public CustomerRepository()
        {
            this.customer_address_repository = new CustomerAddressRepository();
            this.user_repository = new UserRepository();
        }

        /// <summary>
        /// Método retorna uma lista de clientes.
        /// </summary>
        /// <returns>Lista de clientes</returns>
        public List<Customer> GetCustomers()
        {
            List<Models.Customer> customers = new List<Customer>();

            using (Entities entities = new Entities())
            {
                customers = entities.Customer.OrderByDescending(c => c.id).ToList();
            }

            return customers;
        }

        /// <summary>
        /// Método retorna um cliente.
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Objeto</returns>
        public Customer GetCustomer(int id)
        {
            Customer customer = new Customer();

            using (Entities entities = new Entities())
            {
                customer = entities.Customer.Where(c => c.id == id).FirstOrDefault();

                if (customer != null)
                {
                    if (customer.user_id.HasValue)
                    {
                        customer.User = this.user_repository.GetUser(customer.user_id.Value);
                    }

                    customer.CustomerAddress = this.customer_address_repository.GetCustomerAddresses(customer.id);
                }
            }

            return customer;
        }

        /// <summary>
        /// Método retorna um cliente.
        /// </summary>
        /// <param name="id">Identificador do usuário</param>
        /// <returns>Objeto</returns>
        public Customer GetCustomerByUserId(int user_id)
        {
            Customer customer = new Customer();

            using (Entities entities = new Entities())
            {
                customer = entities.Customer.Where(c => c.user_id == user_id).FirstOrDefault();

                if (customer != null)
                {
                    if (customer.user_id.HasValue)
                    {
                        customer.User = this.user_repository.GetUser(customer.user_id.Value);
                    }

                    customer.CustomerAddress = this.customer_address_repository.GetCustomerAddresses(customer.id);
                }
            }

            return customer;
        }

        /// <summary>
        /// Método verifica se existe algum cliente com o telefone.
        /// </summary>
        /// <param name="phone">Telefone do cliente</param>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Verdadeiro ou falso</returns>
        public bool Exists(string phone, int id)
        {
            bool exists = false;

            if (phone != null)
            {
                using (Entities entities = new Entities())
                {
                    exists = entities.Customer.Where(c => c.phone == phone && c.id != id).Count() > 0;
                }
            }

            return exists;
        }

        /// <summary>
        /// Método insere um cliente.
        /// </summary>
        /// <param name="customer">Objeto cliente</param>
        /// <returns>Status da inserção (Verdade ou falso)</returns>
        public int Insert(Customer customer)
        {
            try
            {
                int id = 0;

                customer.enabled = 1;

                using (Entities entities = new Entities())
                {
                    entities.Customer.Add(customer);
                    entities.SaveChanges();
                    id = customer.id;
                }

                return id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método atualiza um cliente.
        /// </summary>
        /// <param name="customer">Objeto cliente</param>
        /// <returns>Status da inserção (Verdade ou falso)</returns>
        public bool Update(Customer customer)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    entities.Entry(customer).State = EntityState.Modified;
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
        /// Método retorna um cliente por telefone.
        /// </summary>
        /// <param name="phone">Telefone do usuário</param>
        /// <returns>Objeto</returns>
        public Customer GetCustomerByPhone(string phone)
        {
            Customer customer = new Customer();

            using (Entities entities = new Entities())
            {
                customer = entities.Customer.Where(c => c.phone.Contains(phone)).FirstOrDefault();

                if (customer != null)
                {
                    if (customer.user_id.HasValue)
                    {
                        customer.User = this.user_repository.GetUser(customer.user_id.Value);
                    }

                    customer.CustomerAddress = this.customer_address_repository.GetCustomerAddresses(customer.id);
                }
            }

            return customer;
        }
    }
}