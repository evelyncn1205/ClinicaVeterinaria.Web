using ClinicaVeterinariaWeb.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ClinicaVeterinariaWeb.Data
{
    public class DataContext  : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DataContext(DbContextOptions<DataContext>options) : base (options)
        {

        }
        public DbSet<ClinicaVeterinariaWeb.Data.Entities.Consulta> Consulta { get; set; }
    }
}
