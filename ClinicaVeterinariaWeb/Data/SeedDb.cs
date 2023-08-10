using ClinicaVeterinariaWeb.Data.Entities;
using ClinicaVeterinariaWeb.Helpers;
using ClinicaVeterinariaWeb.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicaVeterinariaWeb.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private Random _random;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _random = new Random();
            _userHelper= userHelper;
        }



        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Client");
            await _userHelper.CheckRoleAsync("Anonimo");
            await _userHelper.CheckRoleAsync("Employee");


            var user = await _userHelper.GetUserByEmailAsync("evelynrx_rj@hotmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName="Evelyn",
                    LastName="Nunes",
                    Email= "evelynrx_rj@hotmail.com",
                    UserName ="evelynrx_rj@hotmail.com",
                    PhoneNumber = "963258741"
                };
                
                var result = await _userHelper.AddUserAsync(user,"123456");
                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserRoleAsync(user, "Admin");
            }

            var isInRole = await _userHelper.IsUserRoleAsync(user, "Admin");
            if(!isInRole)
            {
                await _userHelper.AddUserRoleAsync(user, "Client");
                await _userHelper.AddUserRoleAsync(user, "Employee");
                await _userHelper.AddUserRoleAsync(user, "Anonimo");
            }

            if (!_context.Clients.Any())
            {
                AddClient("Carlos Alberto","carlosalberto@yopmail.com","Rua do trabalho Dificil 666","Chuchu","Cão","Poodle","8",user);
                AddClient("Maria Silva","maria@yop.mail.com","Av da Saudade 840","Fofinho","Papagaio","Ave","5",user);
                AddClient("Antonio Cardoso","antoniocardoso@yopmail.com","Travessa da libertade 963","Zorba","Cão","Braco Alemão","6",user);
                AddClient("Fátima Rodrigues","fatima@yopmail.com","Av. Quero Praia 600","Negão","Cão","Poodle","12",user);

                await _context.SaveChangesAsync();
            }


            if(! _context.Employees.Any())
            {
                AddEmployees("Margarida", "Campos", "Médica", "margarida@gmail.com", "Rua do Riacho Fundo 80", "5",user);
                AddEmployees("André", "Oliveira", "Médico", "andre@gmail.com", "Rua do Riacho Fundo 80", "1",user);
                AddEmployees("Rosa", "Maria", "Recepcionista", "maria@gmail.com", "Rua do Riacho Fundo 80", "Recepção",user);

                await _context.SaveChangesAsync();
            }


            if (!_context.Consulta.Any())
            {
                AddConsulta("Carlos Alberto", "Chuchu", DateTime.Parse("15-07-2023"), TimeSpan.Parse("10:00"), "Margarida", "Rotina","cão recem operado",user);
                AddConsulta("Maria Silva", "Fofinho", DateTime.Parse("28-07-2023"), TimeSpan.Parse("15:00"), "André", "Vacina","papagaio teve a asa partida",user);
                AddConsulta("Maria Silva", "Zorba", DateTime.Parse("31-07-2023"), TimeSpan.Parse("9:30"), "Margarida", "Rotina","avaliação",user);

                await _context.SaveChangesAsync();
            }


        }

        private void AddConsulta(string nameClient, string animalName, DateTime data, TimeSpan hora, string medico, string tipo,string note, User user)
        {
            _context.Consulta.Add(new Consulta
            {
                AnimalName = animalName,
                ClientName=nameClient,
                Date = data,
                Time= hora,
                Doctor = medico,
                ConsultationType = tipo,
                Note= note,
                CellPhone = "9" + _random.Next(10000000, 99999999).ToString(),
                User = user
            });
        }

        private void AddEmployees(string name, string lastName, string role, string email, string address, string room, User user)
        {
            _context.Employees.Add(new Employee
            {
                Name = name,
                LastName = lastName,
                Role = role,
                Email = email,
                Address = address,
                Room = room,
                Document = _random.Next(10000).ToString("D9"),
                FixedPhone = "2" + _random.Next(10000000, 99999999).ToString(),
                CellPhone = "9" + _random.Next(10000000, 99999999).ToString(),
                User=user
            });
        }

        private void AddClient(string name, string email, string address, string animalName, string especie, string breed, string age, User user)
        {
            _context.Clients.Add(new Client
            {
                Document = _random.Next(10000).ToString("D9"),
                ClientName = name,
                Address= address,
                Email= email,
                Species= especie,
                AnimalName= animalName,
                AnimalAge= age,
                Breed= breed,
                FixedPhone = "2" + _random.Next(10000000, 99999999).ToString(),
                CellPhone = "9" + _random.Next(10000000, 99999999).ToString(),
                User = user
            });
        }



    }
}
