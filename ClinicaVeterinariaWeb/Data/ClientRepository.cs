using ClinicaVeterinariaWeb.Data.Entities;
using ClinicaVeterinariaWeb.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinariaWeb.Data
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly DataContext _context;
            private readonly IUserHelper _userHelper;
        public ClientRepository(DataContext context,
            IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Clients.Include(p => p.User);
        }

        public async Task<IQueryable<Client>> GetClienteAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }
            if (await _userHelper.IsUserRoleAsync(user, "Admin") ||
               await _userHelper.IsUserRoleAsync(user, "Employee"))
              

            {
                return _context.Clients.Include(p => p.User);
            }

            return _context.Clients
                    .Where(c => c.User == user)
                    .OrderByDescending(c => c.FullName);
        }

        public IEnumerable<SelectListItem> GetComboClients()
        {
            var list = _context.Clients.Select(c => new SelectListItem
            {
                Text= c.FullName,
                Value= c.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text="(Selecione o Cliente...)",
                Value="0"
            });

            return list;
        }

        public IEnumerable<SelectListItem> GetComboClienteEmail()
        {
            var list = _context.Clients.Select(p => new SelectListItem
            {
                Text = p.FullName,
                Value = p.Email,
            }).ToList();

            var allEmails = "";
            foreach (var email in list)
            {
                if (allEmails != "")
                {
                    allEmails = allEmails + ",";
                }
                allEmails = allEmails + email.Value.ToString();

            }
            list.Insert(0, new SelectListItem
            {
                Text = "Empty",
                Value = null
            });
            list.Insert(1, new SelectListItem
            {
                Text = "Todos os clientes",
                Value = allEmails
            });

            return list;

        }

        string IClientRepository.GetAnimalName()
        {
            return string.Empty;
        }

        string IClientRepository.GetCellPhone()
        {
            return string.Empty ;

        }

        string IClientRepository.GetEmail()
        {
            return string.Empty;
        }

       
    }
}
