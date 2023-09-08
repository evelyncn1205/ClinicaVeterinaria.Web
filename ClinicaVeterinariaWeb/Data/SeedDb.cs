using ClinicaVeterinariaWeb.Data.Entities;
using ClinicaVeterinariaWeb.Helpers;
using ClinicaVeterinariaWeb.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
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
            await _userHelper.CheckRoleAsync("Employee");
            await _userHelper.CheckRoleAsync("Client");
            //await _userHelper.CheckRoleAsync("Anonimo");

            if (!_context.Countries.Any())
            {
                var cities = new List<City>();
                cities.Add(new City { Name = "Braga" });
                cities.Add(new City { Name = "Guimarães" });
                cities.Add(new City { Name = "Porto" });

                _context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Portugal"
                });

                await _context.SaveChangesAsync();
            }


            var userAdmin = await _userHelper.GetUserByEmailAsync("evelynrx_rj@hotmail.com");
            if (userAdmin == null)
            {
                userAdmin = new User
                {
                    FirstName="Evelyn",
                    LastName="Nunes",
                    Email= "evelynrx_rj@hotmail.com",
                    UserName ="evelynrx_rj@hotmail.com",
                    PhoneNumber = "963258741",
                    Address= "Travessa do Programador, 500 ",
                    CityId = _context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = _context.Countries.FirstOrDefault().Cities.FirstOrDefault()
                };
                
                var result = await _userHelper.AddUserAsync(userAdmin,"123456");

                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserRoleAsync(userAdmin, "Admin");
            }

            var userEmployee = await _userHelper.GetUserByEmailAsync("maria@gmail.com");
            if(userEmployee == null)
            {
                userEmployee= new User
                {
                    FirstName = "Rosa",
                    LastName= "Maria",
                    Email= "maria@gmail.com",
                    UserName="maria@gmail.com",
                    PhoneNumber="963852741",
                    Address ="Rua do Monte Cativo, 43",
                    CityId = _context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = _context.Countries.FirstOrDefault().Cities.FirstOrDefault()
                };
                var result = await _userHelper.AddUserAsync(userEmployee, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserRoleAsync(userEmployee, "Employee");
            }

            var userClient = await _userHelper.GetUserByEmailAsync("carlosalberto@yopmail.com");
            if(userClient == null)
            {
                userClient = new User
                {
                    FirstName= "Carlos",
                    LastName= "Alberto",
                    Email="carlosalberto@yopmail.com",
                    UserName="carlosalberto@yopmail.com",
                    PhoneNumber ="987456321",
                    Address= "Rua Cristovão Colombo, 90",
                    CityId = _context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = _context.Countries.FirstOrDefault().Cities.FirstOrDefault()
                };
                var result = await _userHelper.AddUserAsync(userClient, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserRoleAsync(userClient, "Client");
            }

            var isInRoleAdmin = await _userHelper.IsUserRoleAsync(userAdmin, "Admin");
            var isInRoleEmployee = await _userHelper.IsUserRoleAsync(userEmployee, "Employee");
            var isInRoleClient = await _userHelper.IsUserRoleAsync(userClient, "Client");

            if (!isInRoleAdmin)
            {
                await _userHelper.AddUserRoleAsync(userAdmin, "Admin");
            }

            if(!isInRoleEmployee)
            {
                await _userHelper.AddUserRoleAsync(userEmployee, "Employee");
            }

            if(!isInRoleClient)
            {
                await _userHelper.AddUserRoleAsync(userClient, "Client");
            }

            if (!_context.Clients.Any())
            {
                AddClient("Carlos Alberto","carlosalberto@yopmail.com","Rua do trabalho Dificil 666","Chuchu","Cão","Poodle","8",userAdmin);
                AddClient("Maria Silva","maria@yop.mail.com","Av da Saudade 840","Fofinho","Papagaio","Ave","5",userAdmin);
                AddClient("Antonio Cardoso","antoniocardoso@yopmail.com","Travessa da libertade 963","Zorba","Cão","Braco Alemão","6",userAdmin);
                AddClient("Fátima Rodrigues","fatima@yopmail.com","Av. Quero Praia 600","Negão","Cão","Poodle","12",userAdmin);

                await _context.SaveChangesAsync();
            }


            if(! _context.Employees.Any())
            {
                AddEmployees("Margarida", "Campos", "Médica", "margarida@gmail.com", "Rua do Riacho Fundo 80", "5",userAdmin);
                AddEmployees("André", "Oliveira", "Médico", "andre@gmail.com", "Rua do Riacho Fundo 80", "1",userAdmin);
                AddEmployees("Rosa", "Maria", "Recepcionista", "maria@gmail.com", "Rua do Riacho Fundo 80", "Recepção",userAdmin);

                await _context.SaveChangesAsync();
            }


            if (!_context.Consulta.Any())
            {
                AddConsulta("Carlos Alberto", "Chuchu", DateTime.Parse("15-07-2023"), TimeSpan.Parse("10:00"), "Margarida", "Rotina","cão recem operado",userEmployee);
                AddConsulta("Maria Silva", "Fofinho", DateTime.Parse("28-07-2023"), TimeSpan.Parse("15:00"), "André", "Vacina","papagaio teve a asa partida", userEmployee);
                AddConsulta("Maria Silva", "Zorba", DateTime.Parse("31-07-2023"), TimeSpan.Parse("9:30"), "Margarida", "Rotina","avaliação",userEmployee);

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
