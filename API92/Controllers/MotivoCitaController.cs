using API92.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API92.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MotivoCitaController : Controller
    {

        private readonly IMotivoCitaServices _motivoServices;
        public MotivoCitaController(IMotivoCitaServices motivoCitaServices)
        {

            _motivoServices = motivoCitaServices;
        }

        [HttpGet("{motivoDescripcion}")]
        public async Task<IActionResult> GetMotivoByName(string motivoDescripcion)
        {
            try
            {
                var result = await _motivoServices.GetMotivoByName(motivoDescripcion);
                if (result == null)
                {
                    return NotFound($"No se encontró ningún motivo con la descripción: {motivoDescripcion}");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener el motivo por descripción: {ex.Message}");
            }
        }

    }
}
