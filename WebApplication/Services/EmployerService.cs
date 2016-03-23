using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.Models;
using WebApplication.Repositories;

namespace WebApplication.Services
{
    public interface IEmployerService
    {
        /// <summary>
        /// Método retorna uma lista de funcionários.
        /// </summary>
        /// <returns>Lista de funcionários</returns>
        List<Employer> GetEmployers();

        /// <summary>
        /// Método retorna um funcionário.
        /// </summary>
        /// <param name="id">Identificador do funcionário</param>
        /// <returns>Objeto</returns>
        Employer GetEmployer(int id);

        /// <summary>
        /// Método insere um funcionário.
        /// </summary>
        /// <param name="Employer">Objeto funcionário</param>
        /// <returns>Objeto</returns>
        ReturnStatus Insert(Employer employer);

        /// <summary>
        /// Método insere um funcionário e um usuário.
        /// </summary>
        /// <param name="Employer">Objeto funcionário</param>
        /// <returns>Objeto</returns>
        ReturnStatus Insert(EmployerUser employer_user);

        /// <summary>
        /// Método atualiza um funcionário.
        /// </summary>
        /// <param name="Employer">Objeto funcionário</param>
        /// <returns>Objeto</returns>
        ReturnStatus Update(Employer employer);

        /// <summary>
        /// Método atualiza enabled de um funcionário.
        /// </summary>
        /// <param name="id">Identificador do funcionário</param>
        /// <param name="value">Valor da atualização</param>
        /// <returns>Objeto</returns>
        ReturnStatus SetEnabled(int id, short value);
    }

    public class EmployerService : IEmployerService
    {
        private IEmployerRepository employer_repository;
        private IUserService user_service;

        public EmployerService(IEmployerRepository employer_repository)
        {
            this.employer_repository = employer_repository;
            this.user_service = new UserService(new UserRepository());
        }

        /// <summary>
        /// Método retorna um funcionários
        /// </summary>
        /// <returns>Objeto</returns>
        public Employer GetEmployer(int id)
        {
            return this.employer_repository.GetEmployer(id);
        }

        /// <summary>
        /// Método retorna uma lista de funcionários.
        /// </summary>
        /// <returns>Lista de funcionários.</returns>
        public List<Employer> GetEmployers()
        {
            return this.employer_repository.GetEmployers();
        }

        /// <summary>
        /// Método insere um funcionários
        /// </summary>
        /// <returns>Status da inserção (Verdade ou falso)</returns>
        public ReturnStatus Insert(Employer employer)
        {
            ReturnStatus return_status = new ReturnStatus();

            // Verifica se o telefone pertence a outro funcionários.
            if (this.employer_repository.Exists(employer.phone, employer.id))
            {
                return_status.message = "Telefone já pertence a outro funcionário.";
                return return_status;
            }

            // Verifica se aconteceu alguma erro no cadastro do funcionários.
            if (!this.employer_repository.Insert(employer))
            {
                return_status.message = "Erro ao adicionar funcionário.";
            }

            return_status.success = true;
            return_status.message = "Funcionário adicionado com sucesso.";
            return return_status;
        }

        /// <summary>
        /// Método insere um funcionários
        /// </summary>
        /// <returns>Status da inserção (Verdade ou falso)</returns>
        public ReturnStatus Insert(EmployerUser employer_user)
        {
            ReturnStatus return_status = new ReturnStatus();

            return_status = this.user_service.Insert(employer_user.User);
            if (!return_status.success)
            {
                return_status.message = "Erro ao adicionar usuário do funcionário.";
            }

            employer_user.Employer.user_id = this.user_service.GetUser(employer_user.User.email).id;
            if (this.employer_repository.Insert(employer_user.Employer))
            {
                return_status.success = true;
                return_status.message = "Funcionário adicionado com sucesso";
                return return_status;
            }

            return_status.message = "Erro ao adicionar funcionário.";
            return return_status;
        }

        /// <summary>
        /// Método atualiza um funcionários
        /// </summary>
        /// <returns>Status da atualização (Verdade ou falso)</returns>
        public ReturnStatus Update(Employer employer)
        {
            ReturnStatus return_status = new ReturnStatus();

            // Verifica se o telefone pertence a outro funcionários.            
            if (this.employer_repository.Exists(employer.phone, employer.id))
            {
                return_status.message = "Telefone já pertence a outro funcionário.";
                return return_status;
            }

            // Verifica se aconteceu alguma erro no cadastro do funcionário.
            if (!this.employer_repository.Update(employer))
            {
                return_status.message = "Erro ao editar funcionário.";
                return return_status;
            }

            return_status.success = true;
            return_status.message = "Funcionário editado com sucesso.";
            return return_status;
        }

        /// <summary>
        /// Método atualiza enabled de um funcionário.
        /// </summary>
        /// <param name="id">Identificador do funcionário</param>
        /// <param name="value">Valor da atualização</param>
        /// <returns>Objeto</returns>
        public ReturnStatus SetEnabled(int id, short value)
        {
            ReturnStatus return_status = new ReturnStatus();

            // Obtém o funcionários.
            Employer employer = employer_repository.GetEmployer(id);

            // Verifica se o funcionários existe.
            if (employer == null)
            {
                return_status.message = "Funcionário inexistente.";
                return return_status;
            }

            employer.enabled = value;

            // Atualiza o funcionários.
            if (!employer_repository.Update(employer))
            {
                return_status.message = "Erro ao atualizar status do funcionário.";
                return return_status;
            }

            return_status.success = true;
            return_status.message = "Funcionário atualizado com sucesso.";
            return return_status;
        }
    }
}