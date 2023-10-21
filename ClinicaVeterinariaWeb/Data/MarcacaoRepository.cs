using ClinicaVeterinariaWeb.Data.Entities;
using ClinicaVeterinariaWeb.Helpers;
using ClinicaVeterinariaWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ClinicaVeterinariaWeb.Data
{
    public class MarcacaoRepository : GenericRepository<Marcacao>, iMarcacaoRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly IClientRepository _clientRepository;
        public MarcacaoRepository(DataContext context, IUserHelper userHelper,
            IClientRepository clientRepository) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
            _clientRepository=clientRepository;
        }

        public async Task AddItemMarcacaoAsync(AddMarcacaoViewModel model, string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return;
            }
            var marcacao = await _context.Clients.FindAsync(model.ClienteId);
            if(marcacao == null)
            {
                return ;
            }
            var marcacaoDetailTemp = await _context.MarcacaoDetailsTemp
                .Where(mdt => mdt.User == user && mdt.Client == marcacao)
                .FirstOrDefaultAsync();
            if(marcacaoDetailTemp == null)
            {
                marcacaoDetailTemp = new MarcacaoDetailTemp
                {
                    Client =marcacao,
                    NomeAnimal= model.AnimalName,
                    Data = model.Data,
                    Hora=model.Hora,
                    Email=model.Email,
                    CellPhone=model.CellPhone,
                    Quantity=model.Quantity,
                    TipodaConsulta=model.TipodaConsulta,
                    User=user,
                };
                _context.MarcacaoDetailsTemp.Add(marcacaoDetailTemp);
            }
            else
            {
               
                marcacaoDetailTemp.Quantity += model.Quantity;
                _context.MarcacaoDetailsTemp.Update(marcacaoDetailTemp);
            }

            await _context.SaveChangesAsync();

        }

        public async Task<bool> CancelarMarcacao(CancelarViewModel model)
        {
            var marcacao = await _context.Marcacoes.FindAsync(model.Id);
            if (marcacao == null)
            {
                return false;
            }
            else
            {
                var consulta = _context.Clients.FindAsync(model.Id);
                marcacao.StatusConsulta = StatusConsulta.Cancelada;
                _context.Marcacoes.Update(marcacao);               
                await _context.SaveChangesAsync();
            }

            return true;
        }

        public async Task<Marcacao> ConfirmMarcacaoAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }
            var marcacaoTemp = await _context.MarcacaoDetailsTemp
                .Include(m => m.Client)
                .Where(m => m.User == user)
                .ToListAsync();
           if(marcacaoTemp == null || marcacaoTemp.Count == 0)
           {
                return null;
           }

           var details= marcacaoTemp.Select(m=> new MarcacaoDetail
           {
              Cliente= m.Client,
              NomeAnimal=m.NomeAnimal,
              Data=m.Data,
              Hora=m.Hora,
              CellPhone=m.CellPhone,
              Email=m.Email,
              TipodaConsulta=m.TipodaConsulta,
              Quantity= m.Quantity,              

           }).ToList();

            var marcacao = new Marcacao();

            foreach(MarcacaoDetail marcacaoDetail in details)
            {
                marcacao = new Marcacao
                {
                    Data = marcacaoDetail.Data,
                    User = user,
                    Hora = marcacaoDetail.Hora,
                    Items = details,
                    CellPhone= marcacaoDetail.CellPhone,
                    Cliente= marcacaoDetail.Cliente,
                    Email=marcacaoDetail.Cliente.Email,
                    NomeAnimal=marcacaoDetail.NomeAnimal,
                    TipodaConsulta=marcacaoDetail.TipodaConsulta,
                    Quantity= marcacaoDetail.Quantity,
                    StatusConsulta= StatusConsulta.Ativa

                };
                await CreateAsync(marcacao);
            }
            _context.MarcacaoDetailsTemp.RemoveRange(marcacaoTemp);
            await _context.SaveChangesAsync();

            return marcacao;
        }

        public async Task DeleteDetailTempAsync(int id)
        {
            var marcacaoDetailTemp = await _context.MarcacaoDetailsTemp.FindAsync(id);
            if(marcacaoDetailTemp == null)
            {
                return;
            }
            _context.MarcacaoDetailsTemp.Remove(marcacaoDetailTemp);
            await _context.SaveChangesAsync();
        }

        

        public IQueryable GetAllWithUsers()
        {
            return _context.Clients.Include(m => m.User);
        }

        public DateTime GetData()
        {
            return DateTime.Now;
        }

        public async Task<IQueryable<MarcacaoDetailTemp>> GetDetailstempAsync(string userName)
        {
            var user= await _userHelper.GetUserByEmailAsync(userName);
            
            if (user == null)
            {
                return null;
            }

            return _context.MarcacaoDetailsTemp
                .Include(m => m.Client)
                .Where(o => o.User == user)
                .OrderBy(d => d.Data);
        }

        public TimeSpan GetHora(double hora)
        {
            return TimeSpan.FromHours(hora);
        }

        public async Task<IQueryable<Marcacao>> GetMarcacaoAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if(user == null)
            {
                return null;
            }
            if(await _userHelper.IsUserRoleAsync(user, "Admin") ||
                await _userHelper.IsUserRoleAsync(user, "Employee"))
            {
                return _context.Marcacoes
                    .Include(a=>a.User)
                    .Include(m => m.Items)
                    .ThenInclude(i => i.Cliente)
                    .OrderByDescending(m => m.Data);
            }

            return _context.Marcacoes
                .Include(m => m.Items)
                .ThenInclude(i => i.Cliente)
                .Where(m=>m.User == user)
                .OrderByDescending(m =>m.Data);
        }

        public async Task<Marcacao> GetMarcacaoAsync(int id)
        {
            return await _context.Marcacoes.FindAsync(id);
        }

        public async Task<MarcacaoDetailTemp> GetMarcacaoDetailTempAsync(int id)
        {
            return await _context.MarcacaoDetailsTemp.FindAsync(id);
        }

        public string GetTipoConsulta()
        {
            return string.Empty;
        }

        public async Task<AddMarcacaoViewModel> ReturnMarcacaoViewModel(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            bool role = await _userHelper.IsUserRoleAsync(user, "Client");
            if(role)
            {
               
                var view = new AddMarcacaoViewModel
                {
                    Cliente = _clientRepository.GetComboClients(),
                    AnimalName = _clientRepository.GetAnimalName(),
                    CellPhone = _clientRepository.GetCellPhone(),
                    Email = _clientRepository.GetEmail(),

                };
                return view;
            }
            var model = new AddMarcacaoViewModel
            {
                Cliente = _clientRepository.GetComboClients(),
                AnimalName = _clientRepository.GetAnimalName(),
                CellPhone = _clientRepository.GetCellPhone(),
                Email = _clientRepository.GetEmail(),
            };
            return model;
        }
    }
}




