using ClinicaVeterinariaWeb.Data.Entities;
using ClinicaVeterinariaWeb.Helpers;
using ClinicaVeterinariaWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinariaWeb.Data
{
    public class MarcacaoRepository : GenericRepository<Marcacao>, iMarcacaoRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        public MarcacaoRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
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

        public async Task<bool> ConfirmMarcacaoAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return false;
            }
            var marcacaoTemp = await _context.MarcacaoDetailsTemp
                .Include(m => m.Client)
                .Where(m => m.User == user)
                .ToListAsync();
           if(marcacaoTemp == null || marcacaoTemp.Count == 0)
           {
                return false;
           }

           var details= marcacaoTemp.Select(m=> new MarcacaoDetail
           {
              Client= m.Client,
              NomeAnimal=m.NomeAnimal,
              Data=m.Data,
              Hora=m.Hora,
              CellPhone=m.CellPhone,
              Email=m.Email,
              TipodaConsulta=m.TipodaConsulta,
              Quantity= m.Quantity

           }).ToList();

            foreach(MarcacaoDetail id in details.ToList())
            {
                var marcacao = new Marcacao
                {
                    Data = id.Data,
                    User = user,
                    Hora = id.Hora,
                    Items = details,
                    CellPhone= id.CellPhone,
                    Cliente= id.Client.ClientName,
                    Email=id.Client.Email,
                    NomeAnimal=id.NomeAnimal,
                    TipodaConsulta=id.TipodaConsulta,
                    Quantity= id.Quantity

                };
                await CreateAsync(marcacao);
            }
            _context.MarcacaoDetailsTemp.RemoveRange(marcacaoTemp);
            await _context.SaveChangesAsync();
            return true;
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

        public async Task EditMarcacaoDetailTempAsync(AddMarcacaoViewModel model, string username)
        {
            var user = await _userHelper.GetUserByEmailAsync(username);
            if (user == null)
            {
                return;
            }

            var marcacaoDetailTemp = await _context.MarcacaoDetailsTemp.FindAsync(model.Id);
            if (marcacaoDetailTemp == null)
            {
                return;
            }

            marcacaoDetailTemp.User = user;
            marcacaoDetailTemp.Data = model.Data;
            marcacaoDetailTemp.Hora = model.Hora;
            marcacaoDetailTemp.TipodaConsulta = model.TipodaConsulta;
            marcacaoDetailTemp.NomeAnimal = model.AnimalName;
            marcacaoDetailTemp.CellPhone = model.CellPhone;
            marcacaoDetailTemp.Email=model.Email;
            marcacaoDetailTemp.Quantity=model.Quantity;

            await _context.SaveChangesAsync();

            //var user = await _userHelper.GetUserByEmailAsync(username);
            //if (user == null)
            //{
            //    return;

            //}
            //var marcacao= await _context.Clients.FindAsync(user.Id);
            //if (marcacao == null)
            //{
            //    return ;
            //}

            //var marcacaoDetailTemp = await _context.MarcacaoDetailsTemp.FindAsync(model.Id);
            //marcacaoDetailTemp.User=user;
            //marcacaoDetailTemp.Data= model.Data;
            //marcacaoDetailTemp.Hora=model.Hora;
            //marcacaoDetailTemp.TipodaConsulta=model.TipodaConsulta;
            //marcacaoDetailTemp.NomeAnimal= model.AnimalName;

            //_context.MarcacaoDetailsTemp.Update(marcacaoDetailTemp);

            //await _context.SaveChangesAsync();
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Marcacoes.Include(m => m.User);
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
                .Where(m => m.User == user)
                .OrderBy(m => m.Data);
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
                    .ThenInclude(i => i.Client)
                    .OrderByDescending(m => m.Data);
            }

            return _context.Marcacoes
                .Include(m => m.Items)
                .ThenInclude(i => i.Client)
                .Where(m=>m.User == user)
                .OrderByDescending(m =>m.Data);
        }

        public async Task<Marcacao> GetMarcacaoAync(int id)
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

        public async Task ModifyMarcacaoDetailTempQuantityAsync(int id, double quantity)
        {
            var marcacaoDetailTemp = await _context.MarcacaoDetailsTemp.FindAsync(id);
            if(marcacaoDetailTemp == null)
            {
                return;
            }
            marcacaoDetailTemp.Quantity = quantity;
            if(marcacaoDetailTemp.Quantity > 0)
            {
                _context.MarcacaoDetailsTemp.Update(marcacaoDetailTemp);
                await _context.SaveChangesAsync();
            }
        }
    }
}
