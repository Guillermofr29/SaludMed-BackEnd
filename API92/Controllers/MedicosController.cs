using API92.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API92.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicosController : ControllerBase
    {
        private readonly IMedicoServices _medicoServices;

        public MedicosController(IMedicoServices medicoServices)
        {
            _medicoServices = medicoServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetMedicos()
        {
            try
            {
                var result = await _medicoServices.GetMedicos();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{ID_Cita}")]
        public async Task<IActionResult> GetMedico(int ID_Cita)
        {
            try
            {
                var result = await _medicoServices.GetMedico(ID_Cita);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al obtener al médico por  CitaID: " + ex.Message);
            }
        }
    }
}
