using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public interface IPaymentTypeService
    {
        /// <summary>
        /// Método retorna uma lista de formas de pagamento.
        /// </summary>
        /// <returns>Lista de formas de pagamento.</returns>
        List<PaymentType> GetPaymentTypes();
    }

    public class PaymentTypeService : IPaymentTypeService
    {
        private IPaymentTypeRepository payment_type_repository;

        public PaymentTypeService(IPaymentTypeRepository payment_type_repository)
        {
            this.payment_type_repository = payment_type_repository;
        }

        /// <summary>
        /// Método retorna uma lista de formas de pagamento.
        /// </summary>
        /// <returns>Lista de formas de pagamento.</returns>
        public List<PaymentType> GetPaymentTypes()
        {
            return this.payment_type_repository.GetPaymentTypes();
        }
    }
}