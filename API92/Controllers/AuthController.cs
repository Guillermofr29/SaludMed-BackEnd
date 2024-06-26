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

        // Aquí generarías y retornarías un token JWT en lugar de solo el ID
        return Ok(new { Id = medico.ID_Medico });
    }
}

public class LoginRequest
{
    public string Correo { get; set; }
    public string Contraseña { get; set; }
}

