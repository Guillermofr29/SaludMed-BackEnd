using API92.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API92.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicamentosController : ControllerBase
    {
        private readonly IMedicamentosServices _medicamentosService;

        public MedicamentosController(IMedicamentosServices medicamentosService)
        {
            _medicamentosService = medicamentosService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMedicamentos()
        {
            try
            {
                var result = await _medicamentosService.GetMedicamentos();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener los medicamentos: " + ex.Message);
            }
        }
    }
}
