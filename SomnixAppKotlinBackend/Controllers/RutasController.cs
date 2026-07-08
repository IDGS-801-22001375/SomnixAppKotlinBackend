using Microsoft.AspNetCore.Mvc;
using Somnix.BLL.Interfaces;
using Somnix.DTO.Rutas;

namespace SomnixAppKotlinBackend.Controllers;

[ApiController]
[Route("api/rutas")]
public class RutasController : ControllerBase
{
    private readonly IRutaService _rutaService;

    public RutasController(IRutaService rutaService)
    {
        _rutaService = rutaService;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodas()
    {
        var rutas = await _rutaService.ObtenerTodas();
        return Ok(rutas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(string id)
    {
        var ruta = await _rutaService.ObtenerPorId(id);

        if (ruta == null)
            return NotFound(new { mensaje = "Ruta no encontrada." });

        return Ok(ruta);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] RutaRequest request)
    {
        var ruta = await _rutaService.Crear(request);
        return Ok(ruta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(string id, [FromBody] RutaRequest request)
    {
        var ruta = await _rutaService.Actualizar(id, request);

        if (ruta == null)
            return NotFound(new { mensaje = "Ruta no encontrada." });

        return Ok(ruta);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(string id)
    {
        var eliminado = await _rutaService.Eliminar(id);

        if (!eliminado)
            return NotFound(new { mensaje = "Ruta no encontrada." });

        return Ok(new { mensaje = "Ruta eliminada correctamente." });
    }
}