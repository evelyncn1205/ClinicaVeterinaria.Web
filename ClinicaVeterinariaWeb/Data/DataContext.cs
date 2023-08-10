using ClinicaVeterinariaWeb.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;

namespace ClinicaVeterinariaWeb.Data
{
    public class DataContext  : IdentityDbContext<User>
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DbSet<Comunicacao> Comunicacoes { get; set; }
        public DbSet<Marcacao> Marcacoes { get; set; }
        public DbSet<MarcacaoDetailTemp> MarcacaoDetailsTemp { get; set; }
        public DbSet<MarcacaoDetail> MarcacaoDetails { get; set; }

        public DbSet<ClinicaVeterinariaWeb.Data.Entities.Consulta> Consulta { get; set; }
        public DataContext(DbContextOptions<DataContext>options) : base (options)
        {

        }
        
        
    }
}
