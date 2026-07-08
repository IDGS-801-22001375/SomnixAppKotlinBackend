using Microsoft.AspNetCore.Mvc;
using Somnix.BLL.Interfaces;
using Somnix.DTO.Auth;
using Somnix.DTO.Usuarios;

namespace SomnixAppKotlinBackend.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public AuthController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost("registro")]
    public async Task<IActionResult> Registrar([FromBody] UsuarioRegistroRequest request)
    {
        try
        {
            var usuario = await _usuarioService.Registrar(request);
            return Ok(usuario);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var respuesta = await _usuarioService.Login(request);

            if (respuesta == null)
                return Unauthorized(new { mensaje = "Correo o contraseña incorrectos." });

            return Ok(respuesta);
        }
        catch (Exception ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPost("google")]
    public async Task<IActionResult> LoginGoogle([FromBody] GoogleLoginRequest request)
    {
        var respuesta = await _usuarioService.LoginGoogle(request);

        return Ok(respuesta);
    }
}