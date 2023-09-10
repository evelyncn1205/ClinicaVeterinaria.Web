using ClinicaVeterinariaWeb.Data;
using ClinicaVeterinariaWeb.Data.Entities;
using ClinicaVeterinariaWeb.Helpers;
using ClinicaVeterinariaWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinariaWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly ICountryRepository _countryRepository;

        public AccountController (IUserHelper userHelper, 
            RoleManager<IdentityRole> roleManager,
            UserManager<User> userManager,
            ICountryRepository countryRepository)
        {
            _userHelper = userHelper;
            _roleManager = roleManager;
            _userManager = userManager;
            _countryRepository = countryRepository;
        }

       
        public IActionResult Login()
        {
            if(User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (this.Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(this.Request.Query["ReturnUrl"].First());
                    }
                    return this.RedirectToAction("Index", "Home");
                }
            }
            this.ModelState.AddModelError(string.Empty, "Failed to login");
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

       
        public IActionResult Register()
        {
            var model = new RegisternewUserViewModel
            {
                Countries = _countryRepository.GetComboCountries(),
                Cities = _countryRepository.GetComboCities(0)
            };

            var roles = new RegisternewUserViewModel();
            roles.Roles = new List<SelectListItem>
            {
                  new SelectListItem { Text = "Employee", Value = "Employee" },
                  new SelectListItem { Text = "Client", Value = "Client" },
                  new SelectListItem { Text = "Admin", Value = "Admin" }
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisternewUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var roles = new RegisternewUserViewModel();
                roles.Roles = new List<SelectListItem>
                {
                  new SelectListItem { Text = "Employee", Value = "Employee" },
                  new SelectListItem { Text = "Client", Value = "Client" },
                  new SelectListItem { Text = "Admin", Value = "Admin" }
                };


                var user = await _userHelper.GetUserByEmailAsync(model.Username);
                if (user == null)
                {
                    var city = await _countryRepository.GetCityAsync(model.CityId);

                    user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Username,
                        UserName = model.Username,
                        Address = model.Address,
                        PhoneNumber = model.PhoneNumber,
                        CityId = model.CityId,
                        City = city,
                        Role = model.Role,  
                        
                    };

                    var resut = await _userHelper.AddUserAsync(user, model.Password);
                    if (resut != IdentityResult.Success)
                    {
                        ModelState.AddModelError(string.Empty, "The user couldn't be created");
                        return View();
                    }

                    
                    if (model.Role != null)
                    {
                        var applicationRole = await _roleManager.FindByNameAsync(model.Role);
                        if (applicationRole != null)
                        {
                            IdentityResult roleResult = await _userManager.AddToRoleAsync(user, applicationRole.Name);  
                        }

                        ModelState.AddModelError(string.Empty, "The user couldn't be logged.");
                    }
                }

            }

            return View(model);
        }

        
        public async Task<IActionResult> ChangeUser()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var model = new ChangeUserViewModel();
            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Address = user.Address;
                model.PhoneNumber = user.PhoneNumber;

                var city = await _countryRepository.GetCityAsync(user.CityId);
                if (city != null)
                {
                    var country = await _countryRepository.GetCountryAsync(city);
                    if (country != null)
                    {
                        model.CountryId = country.Id;
                        model.Cities = _countryRepository.GetComboCities(country.Id);
                        model.Countries = _countryRepository.GetComboCountries();
                        model.CityId = user.CityId;
                    }
                }
            }
            model.Cities = _countryRepository.GetComboCities(model.CountryId);
            model.Countries = _countryRepository.GetComboCountries();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var city = await _countryRepository.GetCityAsync(model.CityId);

                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Address = model.Address;
                    user.PhoneNumber = model.PhoneNumber;
                    user.CityId = model.CityId;
                    user.City = city;

                    var response = await _userHelper.UpdateUserAsync(user);
                    if (response.Succeeded)
                    {
                        ViewBag.UserMessage = "User updated!";

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                    }
                }
            }

            return View(model);

        }



        public IActionResult ChangePassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "User not found.");
                }
            }

            return this.View(model);
        }

        public IActionResult NotAuthorized()
        {
            return View();
        }

        [HttpPost]
        [Route("Account/GetCitiesAsync")]
        public async Task<JsonResult> GetCitiesAsync(int countryId)
        {
            var country = await _countryRepository.GetCountryWithCitiesAsync(countryId);
            return Json(country.Cities.OrderBy(c => c.Name));

        }


    }
}
