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

namespace ClinicaVeterinariaWeb.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserHelper _userHelper;


        public ClientsController(IClientRepository clientRepository,
            IUserHelper userHelper)
        {
            _clientRepository = clientRepository;
            _userHelper = userHelper;
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
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\image\\client",
                       file);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/image/client/{file}";
                }

                var client = this.ToClient(model ,path);

               client.User = await _userHelper.GetUserByEmailAsync("evelynrx_rj@hotmail.com");
               await _clientRepository.CreateAsync(client);
               return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        private Client ToClient(ClientViewModel model, string path)
        {
            return new Client
            {
                Id = model.Id,
                AnimalImageUrl = path,
                ClientName = model.ClientName,
                Document=model.Document,
                AnimalAge= model.AnimalAge,
                AnimalName = model.AnimalName,
                CellPhone = model.CellPhone,
                FixedPhone = model.FixedPhone,
                Email = model.Email,
                Address = model.Address,
                Species = model.Species,
                Breed = model.Breed,
                Note = model.Note,
                User = model.User,
            };
             
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

            var model = this.ToClientViewModel(client);
            return View(model);
        }

        private ClientViewModel ToClientViewModel(Client client)
        {
            return new ClientViewModel
            {
                Id = client.Id,
                AnimalImageUrl = client.AnimalImageUrl,
                ClientName = client.ClientName,
                Document=client.Document,
                AnimalAge= client.AnimalAge,
                AnimalName = client.AnimalName,
                CellPhone = client.CellPhone,
                FixedPhone = client.FixedPhone,
                Email = client.Email,
                Address = client.Address,
                Species = client.Species,
                Breed = client.Breed,
                Note = client.Note,
                User = client.User,
            };
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
                        var guid = Guid.NewGuid().ToString();
                        var file = $"{guid}.jpg";

                        path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot\\image\\client",
                           file);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.ImageFile.CopyToAsync(stream);
                        }

                        path = $"~/image/client/{file}";
                    }

                    var client = this.ToClient(model, path);


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
