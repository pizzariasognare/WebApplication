using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;
using WebApplication.Components;

namespace WebApplication.Repositories
{
    public interface ICustomerAddressRepository
    {
        /// <summary>
        /// Método insere um endereço de um cliente.
        /// </summary>
        /// <param name="CustomerAddress">Objeto</param>
        /// <returns>Status da inserção (Verdade ou falso)</returns>
        bool Insert(CustomerAddress CustomerAddress);

        /// <summary>
        /// Método atualiza o endereço de um cliente.
        /// </summary>
        /// <param name="CustomerAddress">Objeto</param>
        /// <returns>Status da atualização (Verdade ou falso)</returns>
        bool Update(CustomerAddress CustomerAddress);

        /// <summary>
        /// Método retorna um endereço.
        /// </summary>
        /// <param name="customer_id">Identificador do endereço.</param>
        /// <returns>Endereços do cliente.</returns>
        CustomerAddress GetCustomerAddress(int id);

        /// <summary>
        /// Método retorna uma lista de endereços.
        /// </summary>
        /// <param name="customer_id">Identificador do cliente.</param>
        /// <returns>Lista de endereços do cliente.</returns>
        List<CustomerAddress> GetCustomerAddresses(int customer_id);

        /// <summary>
        /// Método retorna o último endereço inseriodo de um cliente.
        /// </summary>
        /// <param name="customer_id">Identificador do cliente</param>
        /// <returns>Endereço do cliente.</returns>
        CustomerAddress GetLastCustomerAddress(int customer_id);
    }

    public class CustomerAddressRepository : ICustomerAddressRepository
    {
        private IZipCodeRepository zip_code_repository;        

        public CustomerAddressRepository()
        {
            this.zip_code_repository = new ZipCodeRepository();            
        }

        /// <summary>
        /// Método insere um endereço de um cliente.
        /// </summary>
        /// <param name="CustomerAddress">Objeto</param>
        /// <returns>Status da inserção (Verdade ou falso)</returns>
        public bool Insert(CustomerAddress CustomerAddress)
        {
            try
            {
                CustomerAddress.enabled = 1;

                using (Entities entities = new Entities())
                {
                    entities.CustomerAddress.Add(CustomerAddress);
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
        /// Método atualiza o endereço de um cliente.
        /// </summary>
        /// <param name="CustomerAddress">Objeto</param>
        /// <returns>Status da atualização (Verdade ou falso)</returns>
        public bool Update(CustomerAddress CustomerAddress)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    entities.Entry(CustomerAddress).State = EntityState.Modified;
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
        /// Método retorna uma lista de endereços.
        /// </summary>
        /// <param name="customer_id">Identificador do cliente</param>
        /// <returns>Lista de endereços do cliente.</returns>
        public List<CustomerAddress> GetCustomerAddresses(int customer_id)
        {
            List<CustomerAddress> customer_addresses = new List<CustomerAddress>();

            using (Entities entities = new Entities())
            {
                customer_addresses = entities.CustomerAddress.Where(c => c.customer_id == customer_id).OrderByDescending(c => c.id).ToList();
            }

            return customer_addresses;
        }

        /// <summary>
        /// Método retorna um endereço.
        /// </summary>
        /// <param name="customer_id">Identificador do endereço.</param>
        /// <returns>Endereços do cliente.</returns>
        public CustomerAddress GetCustomerAddress(int id)
        {
            ICustomerRepository customer_repository = new CustomerRepository();

            CustomerAddress customer_address = new CustomerAddress();

            using (Entities entities = new Entities())
            {
                customer_address = entities.CustomerAddress.Where(c => c.id == id).FirstOrDefault();

                if (customer_address != null)
                {
                    customer_address.ZipCode = this.zip_code_repository.GetZipCode(customer_address.zip_code);

                    customer_address.Customer = customer_repository.GetCustomer(customer_address.customer_id);
                }
            }

            return customer_address;
        }

        /// <summary>
        /// Método retorna o último endereço inseriodo de um cliente.
        /// </summary>
        /// <param name="customer_id">Identificador do cliente</param>
        /// <returns>Endereço do cliente.</returns>
        public CustomerAddress GetLastCustomerAddress(int customer_id)
        {
            CustomerAddress customer_address = new CustomerAddress();

            using (Entities entities = new Entities())
            {
                customer_address = entities.CustomerAddress.Where(c => c.customer_id == customer_id && c.enabled == 1).OrderByDescending(c => c.id).FirstOrDefault();

                if (customer_address != null)
                {
                    customer_address.ZipCode = this.zip_code_repository.GetZipCode(customer_address.zip_code);
                }
            }

            return customer_address;
        }
    }
}