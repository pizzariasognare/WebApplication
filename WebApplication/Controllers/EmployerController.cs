using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Repositories;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    public class EmployerController : AppController
    {
        private IEmployerService employer_service;
        private IUserService user_service;

        public EmployerController()
        {
            this.employer_service = new EmployerService(new EmployerRepository());
            this.user_service = new UserService(new UserRepository());
        }

        public EmployerController(IEmployerService employer_service, IUserService user_service)
        {
            this.employer_service = employer_service;
            this.user_service = user_service;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(this.employer_service.GetEmployers());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employer employer)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            ReturnStatus return_status = this.employer_service.Insert(employer);
            if (return_status.success)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", return_status.message);

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Employer employer = this.employer_service.GetEmployer(id);

            if (employer == null)
            {
                return HttpNotFound("Funcionário não encontrado.");
            }

            return View(employer);
        }

        [HttpPost]
        public ActionResult Edit(Employer employer)
        {
            if (!ModelState.IsValid)
            {
                return View(employer);
            }

            ReturnStatus return_status = this.employer_service.Update(employer);
            if (return_status.success)
            {
                return RedirectToAction("Details", "Employer", new { id = employer.id });
            }

            ModelState.AddModelError("", return_status.message);
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            Employer Employer = this.employer_service.GetEmployer(id);

            if (Employer == null)
            {
                return HttpNotFound("Funcionário não encontrado.");
            }

            return View(Employer);
        }

        [HttpPost]
        public JsonResult SetEnabled(int id, short value)
        {
            ReturnStatus return_status = this.employer_service.SetEnabled(id, value);

            return Json(return_status);
        }

        [HttpGet]
        public ActionResult CreateUser(int employer_id)
        {
            Employer employer = this.employer_service.GetEmployer(employer_id);

            // Verifica se o Funcionário existe.
            if (employer == null)
            {
                return HttpNotFound("Funcionário inexistente.");
            }

            // Verifica se o Funcionário está habilitado.
            if (employer.enabled == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Funcionário desabilitado.");
            }

            // Verifica se o Funcionário já tem usuário.
            if (employer.user_id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Funcionário já tem usuário.");
            }

            ViewBag.Employer = employer;

            User user = new User();            

            return View(user);
        }

        [HttpPost]
        public ActionResult CreateUser(int employer_id, User user)
        {            
            Employer employer = this.employer_service.GetEmployer(employer_id);

            // Verifica se o Funcionário existe.
            if (employer == null)
            {
                return HttpNotFound("Funcionário inexistente.");
            }

            // Verifica se o Funcionário está habilitado.
            if (employer.enabled == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Funcionário desabilitado.");
            }

            // Verifica se o Funcionário já tem usuário.
            if (employer.user_id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Funcionário já tem usuário.");
            }            

            ViewBag.Employer = employer;

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            ReturnStatus return_status = this.user_service.Insert(user, employer);
            if (return_status.success)
            {
                return RedirectToAction("Details", "Employer", new { id = employer.id });
            }

            ModelState.AddModelError("", return_status.message);            

            return View(user);
        }

        [HttpGet]
        public ActionResult EditUser(int employer_id)
        {
            Employer employer = this.employer_service.GetEmployer(employer_id);

            // Verifica se o Funcionário existe.
            if (employer == null)
            {
                return HttpNotFound("Funcionário inexistente.");
            }

            // Verifica se o Funcionário está habilitado.
            if (employer.enabled == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Funcionário desabilitado.");
            }

            ViewBag.Employer = employer;

            return View(employer.User);
        }

        [HttpPost]
        public ActionResult EditUser(int employer_id, User user)
        {            
            Employer employer = this.employer_service.GetEmployer(employer_id);

            // Verifica se o Funcionário existe.
            if (employer == null)
            {
                return HttpNotFound("Funcionário inexistente.");
            }            

            // Verifica se o Funcionário está habilitado.
            if (employer.enabled == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Funcionário desabilitado.");
            }

            ViewBag.Employer = employer;

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            ReturnStatus return_status = this.user_service.Update(user);
            if (return_status.success)
            {
                return RedirectToAction("Details", "Employer", new { id = employer.id });
            }

            ModelState.AddModelError("", return_status.message);            

            return View(user);
        }
    }
}
