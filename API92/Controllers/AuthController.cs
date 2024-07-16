using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API92.Context;
using Domain.Entities;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ApplicationDBContext _context;

    public AuthController(ApplicationDBContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var medico = await _context.Medicos
            .FirstOrDefaultAsync(m => m.Correo == request.Correo && m.Contraseña == request.Contraseña);

        if (medico == null)
        {
            return Unauthorized();
        }

        return Ok(new { Id = medico.ID_Medico, ClincaID = medico.ClinicaID, Nombre = medico.Nombre, Apellido = medico.Apellido, Especialidad = medico.Especialidad, CedulaProfesional = medico.CedulaProfesional, Telefono = medico.Telefono, Correo = medico.Correo, RolID = medico.RolID });
    }

    [HttpGet("medico/{id}")]
    public async Task<IActionResult> GetMedico(int id)
    {
        var medico = await _context.Medicos.FindAsync(id);

        if (medico == null)
        {
            return NotFound();
        }

        return Ok(medico);
    }

    [HttpPut("medico/{id}")]
    public async Task<IActionResult> UpdateMedico(int id, [FromBody] MedicoUpdateRequest request)
    {
        var medico = await _context.Medicos.FindAsync(id);

        if (medico == null)
        {
            return NotFound();
        }

        medico.Nombre = request.Nombre;
        medico.Apellido = request.Apellido;
        medico.Especialidad = request.Especialidad;
        medico.CedulaProfesional = request.CedulaProfesional;
        medico.Telefono = request.Telefono;
        medico.Correo = request.Correo;

        if (!string.IsNullOrEmpty(request.Contraseña))
        {
            medico.Contraseña = request.Contraseña;
        }

        await _context.SaveChangesAsync();

        return NoContent();
    }

    public class MedicoUpdateRequest
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Especialidad { get; set; }
        public string CedulaProfesional { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
    }

}

public class LoginRequest
{
    public string Correo { get; set; }
    public string Contraseña { get; set; }
}
