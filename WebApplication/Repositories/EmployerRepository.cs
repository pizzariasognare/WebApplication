using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;
using WebApplication.Components;

namespace WebApplication.Repositories
{
    public interface IEmployerRepository
    {
        /// <summary>
        /// Método retorna uma lista de empregados.
        /// </summary>
        /// <returns>Lista de empregados</returns>
        List<Employer> GetEmployers();

        /// <summary>
        /// Método retorna um empregado.
        /// </summary>
        /// <param name="id">Identificador do empregado</param>
        /// <returns>Objeto</returns>
        Employer GetEmployer(int id);

        /// <summary>
        /// Método verifica se existe algum empregado com o telefone.
        /// </summary>
        /// <param name="phone">Telefone do empregado</param>
        /// <param name="id">Identificador do empregado</param>
        /// <returns>Objeto</returns>
        bool Exists(string phone, int id);

        /// <summary>
        /// Método insere um empregado.
        /// </summary>
        /// <param name="Employer">Objeto empregado</param>
        /// <returns>Status da inserção (Verdade ou falso)</returns>
        bool Insert(Employer Employer);

        /// <summary>
        /// Método atualiza um empregado.
        /// </summary>
        /// <param name="Employer">Objeto empregado</param>
        /// <returns>Status da atualização (Verdade ou falso)</returns>
        bool Update(Employer Employer);
    }

    public class EmployerRepository : IEmployerRepository
    {
        /// <summary>
        /// Método retorna uma lista de empregados.
        /// </summary>
        /// <returns>Lista de empregados</returns>
        public List<Employer> GetEmployers()
        {
            List<Models.Employer> employers = new List<Employer>();

            using (Entities entities = new Entities())
            {
                employers = entities.Employer.OrderByDescending(e => e.id).ToList();
            }

            return employers;
        }

        /// <summary>
        /// Método retorna um empregado.
        /// </summary>
        /// <param name="id">Identificador do empregado</param>
        /// <returns>Objeto</returns>
        public Employer GetEmployer(int id)
        {
            Employer employer = new Employer();

            using (Entities entities = new Entities())
            {
                employer = entities.Employer.Where(e => e.id == id).FirstOrDefault();

                if (employer != null)
                {
                    if (employer.user_id != null)
                    {
                        UserRepository user_serivce = new UserRepository();
                        employer.User = user_serivce.GetUser(employer.user_id.Value);
                    }                    
                }
            }

            return employer;
        }

        /// <summary>
        /// Método verifica se existe algum empregado com o telefone.
        /// </summary>
        /// <param name="phone">Telefone do empregado</param>
        /// <param name="id">Identificador do empregado</param>
        /// <returns>Verdadeiro ou falso</returns>
        public bool Exists(string phone, int id)
        {
            bool exists = false;

            if (phone != null)
            {
                using (Entities entities = new Entities())
                {
                    exists = entities.Employer.Where(c => c.phone == phone && c.id != id).Count() > 0;
                }
            }

            return exists;
        }

        /// <summary>
        /// Método insere um empregado.
        /// </summary>
        /// <returns>Status da inserção (Verdade ou falso)</returns>
        public bool Insert(Employer employer)
        {
            try
            {
                employer.enabled = 1;

                using (Entities entities = new Entities())
                {
                    entities.Employer.Add(employer);
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
        /// Método atualiza um empregado.
        /// </summary>
        /// <param name="Employer">Objeto empregado</param>
        /// <returns>Status da inserção (Verdade ou falso)</returns>
        public bool Update(Employer employer)
        {
            try
            {
                using (Entities entities = new Entities())
                {
                    entities.Entry(employer).State = EntityState.Modified;
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