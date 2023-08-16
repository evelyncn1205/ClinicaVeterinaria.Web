using ClinicaVeterinariaWeb.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClinicaVeterinariaWeb.Data
{
    public interface IClientRepository : IGenericRepository<Client>
    {
        public IQueryable GetAllWithUsers();

        IEnumerable<SelectListItem> GetComboClients();

                
        string GetAnimalName();
        string GetCellPhone();
        string GetEmail();

    }
}
