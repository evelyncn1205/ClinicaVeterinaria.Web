using ClinicaVeterinariaWeb.Data.Entities;
using ClinicaVeterinariaWeb.Helpers;
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
    }
}
