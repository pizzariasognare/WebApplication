using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;

namespace WebApplication.Repositories
{
    public interface IPaymentTypeRepository
    {
        /// <summary>
        /// Método retorna uma lista de tipos de pagamentos.
        /// </summary>
        /// <returns>Lista de tipos de pagamentos.</returns>
        List<PaymentType> GetPaymentTypes();

        /// <summary>
        /// Método retorna um tipo de pagamento.
        /// </summary>
        /// <param name="id">Identificador do tipo de pagamento.</param>
        /// <returns>Objeto</returns>
        PaymentType GetPaymentType(int id);
    }

    public class PaymentTypeRepository : IPaymentTypeRepository
    {
        /// <summary>
        /// Método retorna uma lista de tipos de pagamentos.
        /// </summary>
        /// <returns>Lista de tipos de pagamentos.</returns>
        public List<PaymentType> GetPaymentTypes()
        {
            List<PaymentType> payment_types = new List<PaymentType>();

            using (Entities entities = new Entities())
            {
                payment_types = entities.PaymentType.OrderBy(p => p.id).ToList();
            }

            return payment_types;
        }

        /// <summary>
        /// Método retorna um tipo de pagamento
        /// </summary>
        /// <param name="id">Identificador do tipo de pagamento.</param>
        /// <returns>Objeto</returns>
        public PaymentType GetPaymentType(int id)
        {
            PaymentType payment_type = new PaymentType();

            using (Entities entities = new Entities())
            {
                payment_type = entities.PaymentType.Where(p => p.id == id).FirstOrDefault();
            }

            return payment_type;
        }
    }
}