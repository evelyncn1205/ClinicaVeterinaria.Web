﻿using ClinicaVeterinariaWeb.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaVeterinariaWeb.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : Controller
    {
        private readonly IConsultaRepository _consultaRepository;
        public ConsultasController(IConsultaRepository consultaRepository)
        {
            _consultaRepository = consultaRepository;
        }

        [HttpGet]
        public IActionResult GetConsulta()
        {
            return Ok(_consultaRepository.GetAll());
        }
    }
}
