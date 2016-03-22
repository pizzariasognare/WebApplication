using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public interface IZipCodeService
    {
        /// <summary>
        /// Método retorna uma lista de ceps.
        /// </summary>
        /// <returns>Lista de ceps.</returns>
        List<ZipCode> GetZipCodes();

        /// <summary>
        /// Método retorna um cep.
        /// </summary>
        /// <param name="id">Identificador do CEP</param>
        /// <returns>Objeto</returns>
        ZipCode GetZipCode(int id);

        /// <summary>
        /// Método retorna um cep
        /// </summary>
        /// <param name="zip_code">CEP</param>
        /// <returns>Objeto</returns>
        ZipCode GetZipCode(string zip_code);

        /// <summary>
        /// Método insere um cep.
        /// </summary>
        /// <param name="zip_code">Objeto CEP</param>
        /// <returns>Status da inserção.</returns>
        ReturnStatus Insert(ZipCode zip_code);

        /// <summary>
        /// Método atualiza um cep.
        /// </summary>
        /// <param name="zip_code">Objeto CEP</param>
        /// <returns>Status da atualização.</returns>
        ReturnStatus Update(ZipCode zip_code);
    }

    public class ZipCodeService : IZipCodeService
    {
        private IZipCodeRepository zip_code_repository;

        public ZipCodeService(IZipCodeRepository zip_code_repository)
        {
            this.zip_code_repository = zip_code_repository;
        }

        /// <summary>
        /// Método retorna uma lista de ceps.
        /// </summary>
        /// <returns>Lista de ceps.</returns>
        public List<ZipCode> GetZipCodes()
        {
            return this.zip_code_repository.GetZipCodes();
        }

        /// <summary>
        /// Método retorna um cep.
        /// </summary>
        /// <param name="id">Identificador do CEP</param>
        /// <returns>Objeto</returns>
        public ZipCode GetZipCode(int id)
        {
            return this.zip_code_repository.GetZipCode(id);
        }

        /// <summary>
        /// Método retorna um cep
        /// </summary>
        /// <param name="zip_code">CEP</param>
        /// <returns>Objeto</returns>
        public ZipCode GetZipCode(string zip_code)
        {
            return this.zip_code_repository.GetZipCode(zip_code);
        }

        /// <summary>
        /// Método insere um cep.
        /// </summary>
        /// <param name="zip_code">Objeto CEP</param>
        /// <returns>Status da inserção.</returns>        
        public ReturnStatus Insert(ZipCode zip_code)
        {
            ReturnStatus return_status = new ReturnStatus();

            if (!this.zip_code_repository.Insert(zip_code))
            {
                return_status.message = "Erro ao adicionar CEP.";
                return return_status;
            }

            return_status.message = "CEP inserido com sucesso.";
            return_status.success = true;
            return return_status;
        }

        /// <summary>
        /// Método atualiza um cep.
        /// </summary>
        /// <param name="zip_code">Objeto CEP</param>
        /// <returns>Status da atualização.</returns>        
        public ReturnStatus Update(ZipCode zip_code)
        {
            ReturnStatus return_status = new ReturnStatus();

            if (!this.zip_code_repository.Insert(zip_code))
            {
                return_status.message = "Erro ao atualizar um CEP.";
                return return_status;
            }

            return_status.message = "CEP atualizado com sucesso.";
            return_status.success = true;
            return return_status;
        }
    }
}