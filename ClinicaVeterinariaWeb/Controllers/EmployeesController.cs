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

namespace ClinicaVeterinariaWeb.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserHelper _userHelper;

        public EmployeesController(IEmployeeRepository employeeRepository,
            IUserHelper userHelper)
        {
           _employeeRepository =  employeeRepository;
            _userHelper = userHelper;
        }

        // GET: Employees
        public IActionResult Index()
        {
            return View( _employeeRepository.GetAll().OrderBy(e => e.Name));
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.GetByIdAsync(id.Value);
               
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
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
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\image\\employee",
                        file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/image/employee/{file}";
                }

                var employee = this.ToEmployee(model, path);

                employee.User = await _userHelper.GetUserByEmailAsync("evelynrx_rj@hotmail.com");
                await _employeeRepository.CreateAsync(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        private Employee ToEmployee(EmployeeViewModel model, string path)
        {
            return new Employee
            {
                Id=model.Id,
                ImageUrl = path,
                Name=model.Name,
                LastName=model.LastName,
                Address=model.Address,
                Email=model.Email,
                CellPhone=model.CellPhone,
                FixedPhone=model.FixedPhone,
                Role=model.Role,
                Room=model.Room,
                Document=model.Document,
                User=model.User,

            };
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.GetByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            var model = this.ToEmployeeViewModel(employee);
            return View(model);
        }

        private object ToEmployeeViewModel(Employee employee)
        {
            return new EmployeeViewModel
            {
                Id=employee.Id,
                ImageUrl = employee.ImageUrl,
                Name=employee.Name,
                LastName=employee.LastName,
                Address=employee.Address,
                Email=employee.Email,
                CellPhone=employee.CellPhone,
                FixedPhone=employee.FixedPhone,
                Role=employee.Role,
                Room=employee.Room,
                Document=employee.Document,
                User=employee.User,
            };
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
                        var guid = Guid.NewGuid().ToString();
                        var file = $"{guid}.jpg";

                        path = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot\\image\\employee",
                            file);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await model.ImageFile.CopyToAsync(stream);
                        }

                        path = $"~/image/employee/{file}";
                    }

                    var employee= this.ToEmployee(model, path);
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _employeeRepository.GetByIdAsync(id.Value);
            if (employee == null)
            {
                return NotFound();
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
               
    }
}
