using API92.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace API92.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteServices _pacienteServices;

        public PacienteController(IPacienteServices pacienteServices)
        {
            _pacienteServices = pacienteServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetPacientes(int medicoID)
        {
            try
            {
                var result = await _pacienteServices.GetPacientes(medicoID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener los pacientes: " + ex.Message);
            }
        }


        [HttpGet("PorNombre")]
        public async Task<IActionResult> GetPacientesPorNombre([FromQuery] string nombre, [FromQuery] string apellido)
        {
            try
            {
                var result = await _pacienteServices.GetPacientesPorNombre(nombre, apellido);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener los pacientes por nombre: " + ex.Message);
            }
        }

        [HttpGet("TotalPacientes")]
        public async Task<IActionResult> GetTotalPacientes(int medicoID)
        {
            try
            {
                var result = await _pacienteServices.GetTotalPacientes(medicoID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener el total de pacientes: " + ex.Message);
            }
        }

        [HttpGet("ContarGeneroPacientes")]
        public async Task<IActionResult> ContarGeneroPacientes(int medicoID)
        {
            try
            {
                var result = await _pacienteServices.ContarGeneroPacientes(medicoID);
                return Ok(new {mujeres = result.mujeres, hombres = result.hombres});
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener el conteo de género de pacientes: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearPaciente([FromBody] Pacientes paciente)
        {
            try
            {
                var result = await _pacienteServices.CrearPaciente(paciente);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarPaciente(int id, [FromBody] Pacientes paciente)
        {
            try
            {
                paciente.ID_Paciente = id;
                var result = await _pacienteServices.EditarPaciente(paciente);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarPaciente(int id)
        {
            try
            {
                var result = await _pacienteServices.EliminarPaciente(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPacientePorID(int id)
        {
            try
            {
                var result = await _pacienteServices.GetPacientePorID(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener el paciente por ID: " + ex.Message);
            }
        }

        [HttpGet("PacientesMasRecurrentes")]
        public async Task<IActionResult> GetPacientesMasRecurrentes(int medicoID, int rolID)
        {
            try
            {
                var result = await _pacienteServices.GetPacientesMasRecurrentes(medicoID, rolID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener los pacientes mas recurrentes: " + ex.Message);
            }
        }

    }
}
