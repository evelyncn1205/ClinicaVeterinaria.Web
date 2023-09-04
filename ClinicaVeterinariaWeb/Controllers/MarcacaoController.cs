﻿using ClinicaVeterinariaWeb.Data;
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
            double hora = 2.5;

                var model = new AddMarcacaoViewModel
                {
                    Quantity = 1,
                    Cliente = _clientRepository.GetComboClients(),
                    AnimalName = _clientRepository.GetAnimalName(),
                    CellPhone = _clientRepository.GetCellPhone(),
                    Email = _clientRepository.GetEmail(),
                    Data = _marcacaoRepository.GetData(),
                    Hora = _marcacaoRepository.GetHora(hora),
                    TipodaConsulta= _marcacaoRepository.GetTipoConsulta(),
                };
                

                return View(model);   
        }

        [HttpPost]
        public async Task<IActionResult> AddMarcacao(AddMarcacaoViewModel model)
        {

            if(ModelState.IsValid)
            {
                await _marcacaoRepository.AddItemMarcacaoAsync(model,this.User.Identity.Name);
                return RedirectToAction("Create");
            }

            return View(model);
        }
        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _marcacaoRepository.DeleteDetailTempAsync(id.Value);
            return RedirectToAction("Create");
        }

        public async Task<IActionResult> ConfirmMarcacao()
        {
            var response = await _marcacaoRepository.ConfirmMarcacaoAsync(this.User.Identity.Name);
            if(response)
            {
                return RedirectToAction("Index");   
            }
            return RedirectToAction("Create");
        }
    }
}
