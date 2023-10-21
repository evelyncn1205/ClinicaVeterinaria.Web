using ClinicaVeterinariaWeb.Data;
using ClinicaVeterinariaWeb.Data.Entities;
using ClinicaVeterinariaWeb.Helpers;
using ClinicaVeterinariaWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
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

        public async Task<IActionResult> AddMarcacao(User user)
        {
            user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var model = await _marcacaoRepository.ReturnMarcacaoViewModel(this.User.Identity.Name);
            return View(model);   
        }

        [HttpPost]
        public async Task<IActionResult> AddMarcacao(AddMarcacaoViewModel model)
        {
            
            if(ModelState.IsValid)
            {
                await _marcacaoRepository.AddItemMarcacaoAsync(model, this.User.Identity.Name);                      
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
                Id= editmarcacao.Id,
                Data = editmarcacao.Data,
                Hora=editmarcacao.Hora,
                AnimalName= editmarcacao.NomeAnimal,
                TipodaConsulta= editmarcacao.TipodaConsulta,
                Quantity=editmarcacao.Quantity,
                Cliente=_clientRepository.GetComboClients(),
                CellPhone =editmarcacao.CellPhone,
                Email= editmarcacao.Email
            };
           

            return View(model);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]             
         public async Task<IActionResult> Editar(AddMarcacaoViewModel model)
         {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Data.Date < DateTime.Now)
                    {
                        _flashMessage.Warning("Data Inválida!");
                        return View(model);
                    }

                    
                    var marcacaoDetailTemp = await _context.MarcacaoDetailsTemp.FindAsync(model.Id);

                    if (marcacaoDetailTemp == null)
                    {
                        return new NotFoundViewResult("MarcacaoNotFound");
                    }

                    
                    marcacaoDetailTemp.Data = model.Data;
                    marcacaoDetailTemp.Hora = model.Hora;
                    marcacaoDetailTemp.TipodaConsulta = model.TipodaConsulta;
                    marcacaoDetailTemp.NomeAnimal = model.AnimalName;
                    marcacaoDetailTemp.Quantity = model.Quantity;
                    marcacaoDetailTemp.CellPhone = model.CellPhone;
                    marcacaoDetailTemp.Email = model.Email;

                    
                    _context.MarcacaoDetailsTemp.Update(marcacaoDetailTemp);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Create");
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
            }
            return View(model);
        
        
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

            if (response != null)
            {
                _mailHelper.SendEmail(response.Cliente.Email,
                 "Reserve Confirmed", $"<h1>Clinica Animals Friends</h1>" +
             $"Prezado {response.Cliente.FullName}, " +
                   $"Segue os detalhes da vossa marcação</br></br>" +
                   $"Animal:  {response.Cliente.AnimalName}</br>" +
                   $"Data:  {response.Data}</br>" +
                   $"Hora: {response.Hora}</br>" +
                   $"Para cancelamento ou marcação de nova consulta acesse o nosso site ou entre em contacto.</br>");
                return RedirectToAction("Index");   
              
            }
            return RedirectToAction("Create");
        }

        public async Task<IActionResult>Cancelar(int id)
        {
            var marcacao = await _marcacaoRepository.GetMarcacaoAsync(id);
            if(marcacao.Data < DateTime.UtcNow)
            {
                _flashMessage.Warning("Esta consulta já não pode mais ser cancelada ...");
                return RedirectToAction("index");
            }
            var model = new CancelarViewModel
            {
                Cliente= _clientRepository.GetComboClients()
            };
            return View(model); 
        }

        [HttpPost]
        public async Task<IActionResult>Cancelar(CancelarViewModel model, int id)
        {
            if(ModelState.IsValid)
            {
                var marcacao = await _marcacaoRepository.GetMarcacaoAsync(model.Id);
                if(marcacao.Data < DateTime.UtcNow)
                {
                    _flashMessage.Warning("Esta consulta já não pode mais ser cancelada ...");
                    return RedirectToAction("index");
                }
                var response = await _marcacaoRepository.CancelarMarcacao(model);
                if (response)
                {
                    await EnviarEmailDeCancelamento(model,id);
                    return RedirectToAction("index");
                }
                
            }
            return RedirectToAction("Index");
        }

            
    


        public IActionResult MarcacaoNotFound()
        {
            return View();
        }
               

        public async Task<IActionResult> EnviarEmailDeCancelamento(CancelarViewModel model, int id)
        {
            var marcacao = await _marcacaoRepository.GetMarcacaoAsync(model.Id);
            
            if (marcacao != null)
            {
                // Marque a marcação como cancelada (você deve ter uma propriedade para isso na classe Marcacao)
                marcacao.StatusConsulta = StatusConsulta.Cancelada;

                // Atualize a marcação no contexto do EF
                _context.Marcacoes.Update(marcacao);

                // Envie o email de cancelamento aqui

                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
                string smtpUsername = "evelyncnweb@gmail.com";
                string smtpPassword = "lhloytvbowvqpxxy";

                SmtpClient smtpClient = new SmtpClient(smtpServer)
                {
                    Port = smtpPort,
                    Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                    EnableSsl = true
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpUsername),
                    Subject = "Clinica Veterinária Animals Planet",
                    Body = $"<p>Prezado cliente a consulta do(a) {marcacao.NomeAnimal} marcada para o dia {marcacao.Data} às {marcacao.Hora} foi cancelada.</p> "+
                    "<p>Se pretende marcar uma nova consulta acesse o nosso site ou entre em contacto.</p>"+
                    "<p>Clinica Veterinária Animals Friends.<p>",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(marcacao.Email);

                try
                {
                    // Atualize a marcação no banco de dados
                    await _context.SaveChangesAsync();

                    // Envie o email
                    smtpClient.Send(mailMessage);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Lidere com erros de envio de email aqui
                    return RedirectToAction("ErroAoEnviarEmail");
                }
            }
            else
            {
                // Marcação não encontrada, faça o tratamento apropriado
                return RedirectToAction("MarcaçãoNaoEncontrada");
            }
        }
    }
}
