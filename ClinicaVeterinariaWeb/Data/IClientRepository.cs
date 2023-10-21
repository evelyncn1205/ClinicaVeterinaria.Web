using ClinicaVeterinariaWeb.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinariaWeb.Data
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        public IQueryable GetAllWithUsers();

        IEnumerable<SelectListItem> GetComboClients();

        IEnumerable<SelectListItem> GetComboClienteEmail();
        public Task<IQueryable<Client>> GetClienteAsync(string userName);
        string GetAnimalName();
        string GetCellPhone();
        string GetEmail();

    }
}
