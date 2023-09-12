using ClinicaVeterinariaWeb.Data;
using ClinicaVeterinariaWeb.Data.Entities;
using ClinicaVeterinariaWeb.Helpers;
using ClinicaVeterinariaWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Vereyon.Web;

namespace ClinicaVeterinariaWeb.Controllers
{
    [Authorize]
    public class MarcacaoController : Controller
    {
        private readonly iMarcacaoRepository _marcacaoRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IConverterHelper _converterHelper;
        private readonly IFlashMessage _flashMessage;
        private readonly IUserHelper _userHelper;
        private readonly DataContext _context;
        private readonly IMailHelper _mailHelper;


        public MarcacaoController(iMarcacaoRepository marcacaoRepository,
            IClientRepository clientRepository,
            IConverterHelper converterHelper,
            IFlashMessage flashMessage,
            IUserHelper userHelper,
           DataContext context,
           IMailHelper mailHelper)
        {
            _marcacaoRepository = marcacaoRepository;
            _clientRepository=clientRepository;
            _converterHelper = converterHelper;
            _flashMessage= flashMessage;
            _userHelper=userHelper;
           _context=context;
            _mailHelper=mailHelper;
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
            double hora = 0.0;

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
                return new NotFoundViewResult("MarcacaoNotFound");
            }
            await _marcacaoRepository.DeleteDetailTempAsync(id.Value);
            return RedirectToAction("Create");
        }


        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("MarcacaoNotFound");
            }

            var editmarcacao = await _marcacaoRepository.GetMarcacaoDetailTempAsync(id.Value);
            if (editmarcacao == null)
            {
                return new NotFoundViewResult("MarcacaoNotFound");

            }
            var model = new AddMarcacaoViewModel
            {
                Data = editmarcacao.Data,
                Hora=editmarcacao.Hora,
                AnimalName= editmarcacao.NomeAnimal,
                TipodaConsulta= editmarcacao.TipodaConsulta,

            };
           

            return View(model);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]            

            public async Task<IActionResult> Editar(AddMarcacaoViewModel model)
            {
               
                if (ModelState.IsValid)
                {
                    try
                    {
                        if(model.Data.Date < DateTime.Now)
                        {
                           _flashMessage.Warning("Date Invalid!");
                           model=new AddMarcacaoViewModel
                           { 
                               Cliente=model.Cliente,
                               AnimalName = model.AnimalName,
                               Data = DateTime.Now,
                               Hora=model.Hora,
                               TipodaConsulta= model.TipodaConsulta,
                               Quantity=model.Quantity,
                               CellPhone=model.CellPhone,
                               Email=model.Email,
                           };

                          return View(model); 
                        }
                        else
                        {
                          await _marcacaoRepository.EditMarcacaoDetailTempAsync(model, this.User.Identity.Name);
                        }
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!await _marcacaoRepository.ExistAsync(model.ClienteId))
                        {
                            return new NotFoundViewResult("MarcacaoNotFound");
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction("Create");
                }
                return View(model);
            }
        


        public async Task<IActionResult> Increase(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _marcacaoRepository.ModifyMarcacaoDetailTempQuantityAsync(id.Value, 1);
            return RedirectToAction("Create");

        }

        public async Task<IActionResult> Decrease(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("MarcacaoNotFound");
            }
            await _marcacaoRepository.ModifyMarcacaoDetailTempQuantityAsync(id.Value, -1);
            return RedirectToAction("Create");

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("MarcacaoNotFound");
            }

            var product = await _marcacaoRepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return new NotFoundViewResult("MarcacaoNotFound");
            }

            return View(product);
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

        //[HttpPost]
        //public async Task<IActionResult> ConfirmMarcacao(AddMarcacaoViewModel model, int id)
        //{
        //    var user = await _userHelper.GetUserByEmailAsync(model.Email);
        //    if (user == null)
        //    {
        //        string myToken = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
        //        string tokenLink = Url.Action("ConfirmMarcacaoEmail", "Marcacao", new
        //        {
        //            userid = user.Id,
        //            token = myToken
        //        }, protocol: HttpContext.Request.Scheme);
        //        var marcacao = await _context.Marcacoes.FirstOrDefaultAsync(i => i.Id == id);
        //        Response response2 = _mailHelper.SendEmail(model.Email, "Email confirmation",
        //            $"<h1>Confirmação de Agendamento</h1>" +
        //                              $"<p>Cliente: {marcacao.NomeAnimal}</p>" +
        //                              $"<p>Data: {marcacao.Data}</p>" +
        //                              $"<p>Hora: {marcacao.Hora}</p>" +
        //                              $"<p>Tipo de Consulta: {marcacao.TipodaConsulta}</p>");
        //        if (response2.IsSuccess)
        //        {
        //            ViewBag.Message = "A confirmação da marcação foi envoada por email  ";
        //            return View(model);

        //        }
        //        ModelState.AddModelError(string.Empty, "The user couldn't be logged.");
        //    }
        //    return RedirectToAction("Create");
        //}
        //public async Task<IActionResult> ConfirmMarcacaoEmail(string userId, string token)
        //{
        //    if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
        //    {
        //        return NotFound();
        //    }

        //    var user = await _userHelper.GetUserByIdAsync(userId);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    var result = await _userHelper.ConfirmEmailAsync(user, token);
        //    if (!result.Succeeded)
        //    {

        //    }

        //    return View();

        //}
    }
}
