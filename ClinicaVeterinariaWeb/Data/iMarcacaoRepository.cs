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

        public IQueryable GetAllWithUsers();


        Task<IQueryable<MarcacaoDetailTemp>> GetDetailstempAsync(string userName);
               
        Task AddItemMarcacaoAsync(AddMarcacaoViewModel model, string userName);

        DateTime GetData();
        TimeSpan GetHora(double hora);

        string GetTipoConsulta();

        Task DeleteDetailTempAsync(int id);

        Task<bool> ConfirmMarcacaoAsync(string userName);

        Task<MarcacaoDetailTemp> GetMarcacaoDetailTempAsync(int id);

        Task<Marcacao> GetMarcacaoAync(int id);

        Task ModifyMarcacaoDetailTempQuantityAsync(int id, double quantity);

        Task EditMarcacaoDetailTempAsync(AddMarcacaoViewModel model, string username);
    }
}
