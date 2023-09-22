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
using System.IO;
using ClinicaVeterinariaWeb.Models;
using Microsoft.AspNetCore.Authorization;

namespace ClinicaVeterinariaWeb.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;
        private readonly IConverterHelper _converterHelper;

        public EmployeesController(IEmployeeRepository employeeRepository,
            IUserHelper userHelper,
            IImageHelper imageHelper,
            IConverterHelper converterHelper)
        {
           _employeeRepository =  employeeRepository;
            _userHelper = userHelper;
            _imageHelper = imageHelper;
            _converterHelper = converterHelper;
        }

        // GET: Employees
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View( _employeeRepository.GetAll().OrderBy(e => e.FullName));
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("EmployeeNotFound");
            }

            var employee = await _employeeRepository.GetByIdAsync(id.Value);
               
            if (employee == null)
            {
                return new NotFoundViewResult("EmployeeNotFound");
            }

            return View(employee);
        }

        // GET: Employees/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;
                if (model.ImageFile != null && model.ImageFile.Length >0)
                {
                    path = await _imageHelper.UploadImageAsync(model.ImageFile, "employee");
                }

                var employee = _converterHelper.ToEmployee(model, path, true);

                employee.User = await _userHelper.GetUserByEmailAsync("evelynrx_rj@hotmail.com");
                await _employeeRepository.CreateAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }



        // GET: Employees/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("EmployeeNotFound");
            }

            var employee = await _employeeRepository.GetByIdAsync(id.Value);
            if (employee == null)
            {
                return new NotFoundViewResult("EmployeeNotFound");
            }

            var model = _converterHelper.ToEmployeeViewModel(employee);
            return View(model);
        }

        

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var path = string.Empty;

                    if(model.ImageFile != null && model.ImageFile.Length >0)
                    {
                        path = await _imageHelper.UploadImageAsync(model.ImageFile, "employee");

                    }

                    var employee= _converterHelper.ToEmployee(model, path,false);

                    employee.User= await _userHelper.GetUserByEmailAsync("evelynrx_rj@hotmail.com");
                    await _employeeRepository.UpdateAsync(employee);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _employeeRepository.ExistAsync(model.Id))
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

        // GET: Employees/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("EmployeeNotFound");
            }

            var employee = await _employeeRepository.GetByIdAsync(id.Value);
            if (employee == null)
            {
                return new NotFoundViewResult("EmployeeNotFound");
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            await _employeeRepository.DeleteAsync(employee);
            return RedirectToAction(nameof(Index));
        }
             
        public IActionResult EmployeeNotFound()
        {
            return View();
        }
    }
}
