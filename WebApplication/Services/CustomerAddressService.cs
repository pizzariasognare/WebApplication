using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public interface ICustomerAddressService
    {
        /// <summary>
        /// Método insere um cliente.
        /// </summary>
        /// <param name="CustomerAddress">Objeto cliente</param>
        /// <returns>Objeto</returns>
        ReturnStatus Insert(CustomerAddress CustomerAddress);

        /// <summary>
        /// Método atualiza um cliente.
        /// </summary>
        /// <param name="CustomerAddress">Objeto cliente</param>
        /// <returns>Objeto</returns>
        ReturnStatus Update(CustomerAddress CustomerAddress);

        /// <summary>
        /// Método retorna um endereço.
        /// </summary>
        /// <param name="customer_id">Identificador do endereço.</param>
        /// <returns>Endereços do cliente.</returns>
        CustomerAddress GetCustomerAddress(int id);
    }

    public class CustomerAddressService : ICustomerAddressService
    {
        private ICustomerAddressRepository customer_address_repository;

        public CustomerAddressService(ICustomerAddressRepository customer_address_repository)
        {
            this.customer_address_repository = customer_address_repository;
        }

        /// <summary>
        /// Método insere um endereço de um cliente.
        /// </summary>
        /// <param name="CustomerAddress">Objeto</param>
        /// <returns>Status da inserção (Verdade ou falso)</returns>
        public ReturnStatus Insert(CustomerAddress CustomerAddress)
        {
            ReturnStatus return_status = new ReturnStatus();

            // Verifica se aconteceu alguma erro no cadastro do endereço do cliente.
            if (!this.customer_address_repository.Insert(CustomerAddress))
            {
                return_status.message = "Erro ao adicionar endereço do cliente.";
            }

            return_status.success = true;
            return_status.message = "Endereço do cliente adicionado com sucesso.";
            return return_status;
        }

        /// <summary>
        /// Método atualiza o endereço de um cliente.
        /// </summary>
        /// <param name="CustomerAddress">Objeto</param>
        /// <returns>Status da atualização (Verdade ou falso)</returns>
        public ReturnStatus Update(CustomerAddress CustomerAddress)
        {
            ReturnStatus return_status = new ReturnStatus();

            // Verifica se aconteceu alguma erro no cadastro do cliente.
            if (!this.customer_address_repository.Update(CustomerAddress))
            {
                return_status.message = "Erro ao editar endereço de um cliente.";
                return return_status;
            }

            return_status.success = true;
            return_status.message = "Endereço do cliente editado com sucesso.";
            return return_status;
        }

        /// <summary>
        /// Método retorna um endereço.
        /// </summary>
        /// <param name="customer_id">Identificador do endereço.</param>
        /// <returns>Endereços do cliente.</returns>
        public CustomerAddress GetCustomerAddress(int id)
        {
            return this.customer_address_repository.GetCustomerAddress(id);
        }
    }
}