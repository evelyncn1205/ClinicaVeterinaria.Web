using ClinicaVeterinariaWeb.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ClinicaVeterinariaWeb.Data
{
    public class DataContext  : IdentityDbContext<User>
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Contacto> Contactos { get; set; }
        public DataContext(DbContextOptions<DataContext>options) : base (options)
        {

        }
        public DbSet<ClinicaVeterinariaWeb.Data.Entities.Consulta> Consulta { get; set; }
    }
}
