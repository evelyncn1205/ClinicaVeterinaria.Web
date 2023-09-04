using ClinicaVeterinariaWeb.Data.Entities;
using ClinicaVeterinariaWeb.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinariaWeb.Data
{
    public interface iMarcacaoRepository: IGenericRepository<Marcacao>
    {
        Task<IQueryable<Marcacao>> GetMarcacaoAsync(string userName);

        Task<IQueryable<MarcacaoDetailTemp>> GetDetailstempAsync(string userName);
               
        Task AddItemMarcacaoAsync(AddMarcacaoViewModel model, string userName);

        DateTime GetData();
        TimeSpan GetHora(double hora);

        string GetTipoConsulta();

        Task DeleteDetailTempAsync(int id);
        Task<bool> ConfirmMarcacaoAsync(string userName);

        Task ModifyReserveDetailTempQuantityAsync(int id, double quantity);
    }
}
