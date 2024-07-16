using API92.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API92.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecetasController : ControllerBase
    {
        private readonly IRecetaService _recetaService;

        public RecetasController(IRecetaService recetaService)
        {
            _recetaService = recetaService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearReceta([FromBody] Receta receta)
        {
            try
            {
                int idReceta = await _recetaService.CrearRecetaConMedicamentos(receta);
                return Ok(new { ID_Receta = idReceta });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("Recetas")]
        public async Task<IActionResult> GetResetas(int medicoID, int rolID)
        {
            try
            {
                var result = await _recetaService.GetRecetas(medicoID, rolID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener las recetas: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarRecta(int id)
        {
            try
            {
                var result = await _recetaService.EliminarReceta(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
