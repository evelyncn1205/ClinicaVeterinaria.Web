using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaVeterinariaWeb.Data;
using ClinicaVeterinariaWeb.Data.Entities;
using ClinicaVeterinariaWeb.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ClinicaVeterinariaWeb.Controllers
{
    public class ConsultasController : Controller
    {
        private readonly IConsultaRepository _consultaRepository;
        private readonly IUserHelper _userHelper;

        public ConsultasController(IConsultaRepository consultaRepository,
            IUserHelper userHelper)
        {
            _consultaRepository = consultaRepository;   
            _userHelper = userHelper;
        }

        // GET: Consultas
        //[Authorize(Roles = "Employee,Client,Admin")]
        public IActionResult Index()
        {
            return View(_consultaRepository.GetAll().OrderBy(e => e.Date));
        }

        // GET: Consultas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ConsultaNotFound");
            }

            var consulta = await _consultaRepository.GetByIdAsync(id.Value);
            if (consulta == null)
            {
                return new NotFoundViewResult("ConsultaNotFound");
            }

            return View(consulta);
        }

        // GET: Consultas/Create
        [Authorize(Roles = "Employee,Client")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consultas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                consulta.User= await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await _consultaRepository.CreateAsync(consulta);
                return RedirectToAction(nameof(Index));
            }
            return View(consulta);
        }

        // GET: Consultas/Edit/5
        [Authorize(Roles = "Employee,Client")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ConsultaNotFound");
            }

            var consulta = await _consultaRepository.GetByIdAsync(id.Value);
            if (consulta == null)
            {
                return new NotFoundViewResult("ConsultaNotFound");
            }
            return View(consulta);
        }

        // POST: Consultas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Consulta consulta)
        {
            if (id != consulta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    consulta.User= await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await _consultaRepository.UpdateAsync(consulta);  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _consultaRepository.ExistAsync(consulta.Id))
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
            return View(consulta);
        }

        // GET: Consultas/Delete/5
        [Authorize(Roles = "Employee,Client")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ConsultaNotFound");
            }

            var consulta = await _consultaRepository.GetByIdAsync(id.Value);
            if (consulta == null)
            {
                return NotFound(); return new NotFoundViewResult("ConsultaNotFound");
            }

            return View(consulta);
        }

        // POST: Consultas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consulta = await _consultaRepository.GetByIdAsync(id);
                 
            await _consultaRepository.DeleteAsync(consulta);
            return RedirectToAction(nameof(Index));
                     

        }

       
        public IActionResult ConsultaNotFound()
        {
            return View();
        }
    }
}
