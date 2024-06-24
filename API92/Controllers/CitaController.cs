using API92.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API92.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitaController : ControllerBase
    {
        private readonly ICitaServices _citasServices;
        public CitaController(ICitaServices citaServices)
        {

            _citasServices = citaServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetCitas()
        {
            try
            {
                var result = await _citasServices.GetCitas();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CrearCitas([FromBody] Citas citas)
        {
            try
            {
                var result = await _citasServices.CrearCitas(citas);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarCitas(int id, [FromBody] Citas citas)
        {
            try
            {
                citas.ID_Cita = id;
                var result = await _citasServices.EditarCitas(citas);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCitas(int id)
        {
            try
            {
                var citas = new Citas { ID_Cita = id };
                var result = await _citasServices.EliminarCitas(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("TotalCitas")]
        public async Task<IActionResult> GetTotalCitas()
        {
            try
            {
                var result = await _citasServices.GetTotalCitas();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener el total de citas: " + ex.Message);
            }
        }

        [HttpGet("CitasPendientes")]
        public async Task<IActionResult> GetCitasPendientes()
        {
            try
            {
                var result = await _citasServices.GetCitasPendientes();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener las citas pendientes: " + ex.Message);
            }
        }

        [HttpGet("UltimasCincoCitas")]
        public async Task<IActionResult> GetUltimasCincoCitas()
        {
            try
            {
                var result = await _citasServices.GetUltimasCincoCitas();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener las últimas cinco citas: " + ex.Message);
            }
        }

        [HttpGet("CincoMedicamentosMasRecetados")]
        public async Task<IActionResult> GetCincoMedicamentosMasRecetados()
        {
            try
            {
                var result = await _citasServices.GetCincoMedicamentosMasRecetados();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener los medicamentos más recetados: " + ex.Message);
            }
        }

        [HttpGet("CincoMotivosConsultaMasComunes")]
        public async Task<IActionResult> GetCincoMotivosConsultaMasComunes()
        {
            try
            {
                var result = await _citasServices.GetCincoMotivosConsultaMasComunes();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener los motivos de consulta más comunes: " + ex.Message);
            }
        }

    }
}
