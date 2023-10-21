using ClinicaVeterinariaWeb.Data;
using ClinicaVeterinariaWeb.Data.Entities;
using ClinicaVeterinariaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using ClinicaVeterinariaWeb.Helpers;
using Vereyon.Web;

namespace ClinicaVeterinariaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        private readonly IClientRepository _clientRepository;
        private readonly IMailHelper _mailHelper;
        private readonly IFlashMessage _flashMessage;

        public HomeController(ILogger<HomeController> logger, DataContext context,
            IClientRepository clientRepository,
           IMailHelper  mailHelper,
           IFlashMessage flashMessage)
        {
            _logger = logger;
            _context = context; 
            _clientRepository= clientRepository;
            _mailHelper= mailHelper;
            _flashMessage= flashMessage;
        }

        public IActionResult Index()
        {
            return View();
        }  

        public IActionResult Sobre()
        {
            return View();
        }

        public IActionResult Servicos()
        {
            return View();
        }         
       
        [Authorize(Roles = "Employee, Admin")]
        public IActionResult Comunicacao()
        {
            var model = new ContatosViewModel
            {
                Clientes= _clientRepository.GetComboClienteEmail()
            };
            return View(model);
        }

        [HttpPost]
        public  IActionResult Comunicacao(ContatosViewModel model )
        {
            Response response = _mailHelper.SendEmail(model.Email, model.Subject, model.Mensagem);
            _context.Contactos.Add(model);
            _context.SaveChangesAsync();

            if (response.IsSuccess)
            {

                _flashMessage.Confirmation("Email enviado com sucesso !!");
                model = new ContatosViewModel
                {
                    Clientes = _clientRepository.GetComboClienteEmail(),

                };
                return View(model);

            }

            model = new ContatosViewModel
            {
                Clientes = _clientRepository.GetComboClienteEmail(),
            };
            return View(model);
                        
        }

        public IActionResult Contactos()
        {
            return View();
        }

        [HttpPost]
        public  IActionResult Contactos(ContatosViewModel model)
        {
            Response response = _mailHelper.SendEmail("rosamaria@yopmail.com",model.Subject,model.Mensagem);
            _context.Contactos.Add(model);
            _context.SaveChangesAsync();
            if (response.IsSuccess)
            {

                _flashMessage.Confirmation("Email enviado com sucesso !!!");
                return RedirectToAction("Index");

            }

            return RedirectToAction("Index");
        }


    }
}
