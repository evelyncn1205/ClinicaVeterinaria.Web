using ClinicaVeterinariaWeb.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicaVeterinariaWeb.Data
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        private readonly DataContext _context;
        public ClientRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Clients.Include(c => c.User);
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
