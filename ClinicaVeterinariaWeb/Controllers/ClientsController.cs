using System;
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
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Employee, Admin")]
        public IActionResult Index()
        {
            return View(_clientRepository.GetAll().OrderBy(e => e.FullName));
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ClientNotFound");
            }

            var client = await _clientRepository.GetByIdAsync(id.Value);
            if (client == null)
            {
                return new NotFoundViewResult("ClientNotFound");
            }

            return View(client);
        }

        // GET: Clients/Create
        //[Authorize(Roles = "Employee")]
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

               client.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
               await _clientRepository.CreateAsync(client);
               return RedirectToAction(nameof(Index));
            }
            return View(model);
        }


        // GET: Clients/Edit/5
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ClientNotFound");
            }

            var client = await _clientRepository.GetByIdAsync(id.Value);
            if (client == null)
            {
                return new NotFoundViewResult("ClientNotFound");
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

                    client.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
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
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ClientNotFound");
            }

            var client = await _clientRepository.GetByIdAsync(id.Value);
            if (client == null)
            {
                return new NotFoundViewResult("ClientNotFound");
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);

            try
            {
                await _clientRepository.DeleteAsync(client);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {

                if (ex.InnerException != null && ex.InnerException.Message.Contains("DELETE"))
                {
                    ViewBag.ErrorTitle = $"{client.FullName} provavelmente está a ser usado!!!";
                    ViewBag.ErrorMessage = $"{client.FullName} não pode ser apagado, existem consultas marcadas para este cliente.</br></br>" +
                        $"Primeiro exclua as consultas marcadas" +
                        $" e tente apagar novamente";

                }

                return View("Error");
            }


        }

        public IActionResult ClientNotFound()
        {
            return View();
        }
    }
}
