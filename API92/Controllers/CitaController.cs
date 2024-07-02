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
        public async Task<IActionResult> GetCitas(int medicoID)
        {
            try
            {
                var result = await _citasServices.GetCitas(medicoID);
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
        public async Task<IActionResult> GetTotalCitas(int medicoID)
        {
            try
            {
                var result = await _citasServices.GetTotalCitas(medicoID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener el total de citas: " + ex.Message);
            }
        }

        [HttpGet("CitasPendientes")]
        public async Task<IActionResult> GetCitasPendientes(int medicoID)
        {
            try
            {
                var result = await _citasServices.GetCitasPendientes(medicoID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener las citas pendientes: " + ex.Message);
            }
        }

        [HttpGet("UltimasCincoCitas")]
        public async Task<IActionResult> GetUltimasCincoCitas(int medicoID)
        {
            try
            {
                var result = await _citasServices.GetUltimasCincoCitas(medicoID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener las últimas cinco citas: " + ex.Message);
            }
        }

        //[HttpGet("CincoMedicamentosMasRecetados")]
        //public async Task<IActionResult> GetCincoMedicamentosMasRecetados()
        //{
        //    try
        //    {
        //        var result = await _citasServices.GetCincoMedicamentosMasRecetados();
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest("Error al obtener los medicamentos más recetados: " + ex.Message);
        //    }
        //}

        [HttpGet("CincoMotivosConsultaMasComunes")]
        public async Task<IActionResult> GetCincoMotivosConsultaMasComunes(int medicoID)
        {
            try
            {
                var result = await _citasServices.GetCincoMotivosConsultaMasComunes(medicoID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener los motivos de consulta más comunes: " + ex.Message);
            }
        }

        [HttpGet("ProximasCitas")]
        public async Task<IActionResult> GetProximasCitas(int medicoID, int rolID)
        {
            try
            {
                var result = await _citasServices.GetProximasCitas(medicoID, rolID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener los motivos de consulta más comunes: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCitaPorID(int id)
        {
            try
            {
                var result = await _citasServices.GetCitaPorID(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener la cita por ID: " + ex.Message);
            }
        }

    }
}
