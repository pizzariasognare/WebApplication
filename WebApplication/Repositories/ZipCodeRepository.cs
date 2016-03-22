using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IZipCodeRepository
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
        bool Insert(ZipCode zip_code);

        /// <summary>
        /// Método atualiza um cep.
        /// </summary>
        /// <param name="zip_code">Objeto CEP</param>
        /// <returns>Status da atualização.</returns>
        bool Update(ZipCode zip_code);
    }

    public class ZipCodeRepository : IZipCodeRepository
    {
        /// <summary>
        /// Método retorna uma lista de ceps.
        /// </summary>
        /// <returns>Lista de ceps.</returns>
        public List<ZipCode> GetZipCodes()
        {
            List<ZipCode> zip_codes = new List<ZipCode>();

            using (Entities entities = new Entities())
            {
                zip_codes = entities.ZipCode.OrderBy(p => p.zip_code).ToList();
            }

            return zip_codes;
        }

        /// <summary>
        /// Método retorna um cep.
        /// </summary>
        /// <param name="id">Identificador do cep</param>
        /// <returns>Objeto</returns>
        public ZipCode GetZipCode(int id)
        {
            ZipCode zip_code = new ZipCode();

            using (Entities entities = new Entities())
            {
                zip_code = entities.ZipCode.Where(p => p.id == id).FirstOrDefault();
            }

            return zip_code;
        }

        /// <summary>
        /// Método retorna um cep.
        /// </summary>
        /// <param name="zip_code">CEP</param>
        /// <returns>Objeto</returns>
        public ZipCode GetZipCode(string zip_code)
        {
            ZipCode _zip_code = new ZipCode();

            using (Entities entities = new Entities())
            {
                _zip_code = entities.ZipCode.Where(p => p.zip_code == zip_code).FirstOrDefault();
            }

            return _zip_code;
        }

        /// <summary>
        /// Método insere um cep.
        /// </summary>
        /// <param name="zip_code">Objeto CEP</param>
        /// <returns>Status da inserção.</returns>
        public bool Insert(ZipCode zip_code)
        {
            try
            {
                zip_code.delivery_price = Convert.ToDecimal(2.00);

                using (Entities entities = new Entities())
                {
                    entities.ZipCode.Add(zip_code);
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
        /// Método atualiza um cep.
        /// </summary>
        /// <param name="zip_code">Objeto CEP</param>
        /// <returns>Status da atualização.</returns>
        public bool Update(ZipCode zip_code)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    entities.Entry(zip_code).State = EntityState.Modified;
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