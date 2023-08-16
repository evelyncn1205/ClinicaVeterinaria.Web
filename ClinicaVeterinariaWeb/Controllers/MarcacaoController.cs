using ClinicaVeterinariaWeb.Data;
using ClinicaVeterinariaWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ClinicaVeterinariaWeb.Controllers
{
    [Authorize]
    public class MarcacaoController : Controller
    {
        private readonly iMarcacaoRepository _marcacaoRepository;
        private readonly IClientRepository _clientRepository;

        public MarcacaoController(iMarcacaoRepository marcacaoRepository, IClientRepository clientRepository)
        {
            _marcacaoRepository = marcacaoRepository;
            _clientRepository=clientRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            var model = await _marcacaoRepository.GetMarcacaoAsync(this.User.Identity.Name);

            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _marcacaoRepository.GetDetailstempAsync(this.User.Identity.Name);

            return View(model);
        }

        public IActionResult AddMarcacao()
        {              
            
                var model = new AddMarcacaoViewModel
                {
                    Cliente = _clientRepository.GetComboClients(),
                    AnimalName = _clientRepository.GetAnimalName(),
                    CellPhone = _clientRepository.GetCellPhone(),
                    Email = _clientRepository.GetEmail()
                };
                

                return View(model);
        }


    }
}
