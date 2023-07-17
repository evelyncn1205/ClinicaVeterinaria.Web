﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaVeterinariaWeb.Data;
using ClinicaVeterinariaWeb.Data.Entities;
using Microsoft.CodeAnalysis.CSharp;
using ClinicaVeterinariaWeb.Helpers;
using ClinicaVeterinariaWeb.Models;
using System.IO;

namespace ClinicaVeterinariaWeb.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public ClientsController(IClientRepository clientRepository,
            IUserHelper userHelper,
            IImageHelper imageHelper,
            IConverterHelper converterHelper)
        {
            _clientRepository = clientRepository;
            _userHelper = userHelper;
            _imageHelper=imageHelper;
            _converterHelper = converterHelper;
        }

        // GET: Clients
        public IActionResult Index()
        {
            return View(_clientRepository.GetAll().OrderBy(e => e.ClientName));
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientRepository.GetByIdAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if(model.ImageFile != null && model.ImageFile.Length >0)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "client");
                }

                
                var client= _converterHelper.ToClient(model, path, true);

               client.User = await _userHelper.GetUserByEmailAsync("evelynrx_rj@hotmail.com");
               await _clientRepository.CreateAsync(client);
               return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        
        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientRepository.GetByIdAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            
            var model = _converterHelper.ToClientViewModel(client);
            return View(model);
        }

       

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClientViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    var path = string.Empty;
                    if (model.ImageFile != null && model.ImageFile.Length >0)
                    {
                        path = await _imageHelper.UploadImageAsync(model.ImageFile, "client");
                    }

                    var client = _converterHelper.ToClient(model, path, false);

                    client.User = await _userHelper.GetUserByEmailAsync("evelynrx_rj@hotmail.com");
                    await _clientRepository.UpdateAsync(client);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _clientRepository.ExistAsync(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientRepository.GetByIdAsync(id.Value);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            await _clientRepository.DeleteAsync(client);
            return RedirectToAction(nameof(Index));
        }

        
    }
}
