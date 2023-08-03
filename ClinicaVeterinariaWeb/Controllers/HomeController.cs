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
using System.Security.Policy;
using System.Threading.Tasks;

namespace ClinicaVeterinariaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context; 
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

        [HttpPost]
        public async Task<IActionResult> Contactos(string nome, string email, string mensagem)
        {
            if (ModelState.IsValid)
            {
                var contacto = new Contacto
                {
                    Name = nome,
                    Email = email,
                    Mensagem = mensagem
                };
                _context.Contactos.Add(contacto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Servicos));
            }

            return RedirectToAction();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Comunicacao()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Comunicacao(string nome, string email, string mensagem )
        {
            if (ModelState.IsValid)
            {
                var comunicacao = new Comunicacao
                {
                    Name = nome,
                    Email = email,
                    Mensagem = mensagem
                };
                _context.Comunicacoes.Add(comunicacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Servicos));
            }

            return RedirectToAction();
        }

        [Route("error/404")]
        public IActionResult Error404()
        {
            return View();
        }

    }
}
