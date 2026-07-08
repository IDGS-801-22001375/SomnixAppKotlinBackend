using Microsoft.AspNetCore.Mvc;
using Somnix.BLL.Interfaces;

namespace SomnixAppKotlinBackend.Controllers;

[ApiController]
[Route("api/alertas")]
public class AlertasController : ControllerBase
{
    private readonly IAlertaService _alertaService;

    public AlertasController(IAlertaService alertaService)
    {
        _alertaService = alertaService;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodas()
    {
        var alertas = await _alertaService.ObtenerTodas();
        return Ok(alertas);
    }

    [HttpGet("usuario/{usuarioId}")]
    public async Task<IActionResult> ObtenerPorUsuario(string usuarioId)
    {
        var alertas = await _alertaService.ObtenerPorUsuario(usuarioId);
        return Ok(alertas);
    }

    [HttpGet("ruta/{rutaId}")]
    public async Task<IActionResult> ObtenerPorRuta(string rutaId)
    {
        var alertas = await _alertaService.ObtenerPorRuta(rutaId);
        return Ok(alertas);
    }

    [HttpPut("{id}/leer")]
    public async Task<IActionResult> MarcarComoLeida(string id)
    {
        var resultado = await _alertaService.MarcarComoLeida(id);

        if (!resultado)
            return NotFound(new { mensaje = "Alerta no encontrada." });

        return Ok(new { mensaje = "Alerta marcada como leída." });
    }
}