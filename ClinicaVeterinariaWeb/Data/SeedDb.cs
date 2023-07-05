using ClinicaVeterinariaWeb.Data.Entities;
using ClinicaVeterinariaWeb.Migrations;
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

        public SeedDb(DataContext context)
        {
            _context = context;
            _random = new Random();
        }



        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            if (!_context.Clients.Any())
            {
                AddClient("Carlos Alberto","carlosalberto@yopmail.com","Rua do trabalho Dificil 666","Chuchu","Cão","Poodle","8");
                AddClient("Maria Silva","maria@yop.mail.com","Av da Saudade 840","Fofinho","Papagaio","Ave","5");
                AddClient("Antonio Cardoso","antoniocardoso@yopmail.com","Travessa da libertade 963","Zorba","Cão","Braco Alemão","6");
                AddClient("Fátima Rodrigues","fatima@yopmail.com","Av. Quero Praia 600","Negão","Cão","Poodle","12");

                await _context.SaveChangesAsync();
            }


            if(! _context.Employees.Any())
            {
                AddEmployees("Margarida", "Campos", "Médica", "margarida@gmail.com", "Rua do Riacho Fundo 80", "5");
                AddEmployees("André", "Oliveira", "Médico", "andre@gmail.com", "Rua do Riacho Fundo 80", "1");
                AddEmployees("Rosa", "Maria", "Recepcionista", "maria@gmail.com", "Rua do Riacho Fundo 80", "Recepção");

                await _context.SaveChangesAsync();
            }


            if (!_context.Consulta.Any())
            {
                AddConsulta("Carlos Alberto", "Chuchu", DateTime.Parse("15-07-2023"), TimeSpan.Parse("10:00"), "Margarida", "Rotina","cão recem operado");
                AddConsulta("Maria Silva", "Fofinho", DateTime.Parse("28-07-2023"), TimeSpan.Parse("15:00"), "André", "Vacina","papagaio teve a asa partida");
                AddConsulta("Maria Silva", "Zorba", DateTime.Parse("31-07-2023"), TimeSpan.Parse("9:30"), "Margarida", "Rotina","avaliação");

                await _context.SaveChangesAsync();
            }


        }

        private void AddConsulta(string nameClient, string animalName, DateTime data, TimeSpan hora, string medico, string tipo,string note)
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
            });
        }

        private void AddEmployees(string name, string lastName, string role, string email, string address, string room)
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
            });
        }

        private void AddClient(string name, string email, string address, string animalName, string especie, string breed, string age)
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

            });
        }



    }
}
