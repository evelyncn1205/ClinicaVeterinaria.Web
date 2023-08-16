using ClinicaVeterinariaWeb.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinariaWeb.Data
{
    public interface iMarcacaoRepository: IGenericRepository<Marcacao>
    {
        Task<IQueryable<Marcacao>> GetMarcacaoAsync(string userName);

        Task<IQueryable<MarcacaoDetailTemp>> GetDetailstempAsync(string userName);
               
    }
}
