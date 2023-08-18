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
                .Where(mdt => mdt.User == user && mdt.Client ==marcacao)
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
                .Where(i => i.User == user)
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
            if(await _userHelper.IsUserRoleAsync(user,"Admin,Employee"))
            {
                return _context.Marcacoes
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

        public string GetTipoConsulta()
        {
            return string.Empty;
        }
    }
}
