using Microsoft.AspNetCore.Mvc;
using Somnix.BLL.Services;
using Somnix.DTO.Telemetria;

namespace SomnixAppKotlinBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelemetriaController : ControllerBase
    {
        private readonly FirebaseService _firebaseService;

        public TelemetriaController(FirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        // POST: /api/Telemetria
        [HttpPost]
        public async Task<IActionResult> RegistrarTelemetria([FromBody] TelemetriaDto payload)
        {
            if (payload == null) return BadRequest("Payload vacío.");

            var telemetriaExt = new TelemetriaFirebase
            {
                Pitch = payload.p,
                Roll = payload.r,
                NivelAlerta = payload.n,
                ModoTest = payload.t == 1,
                EnViaje = payload.v == 1,
                LastHttpCode = payload.hc,
                FechaRegistro = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.ffffffzzz"),
                TiempoReaccionMs = payload.tr
            };

            await _firebaseService.InsertarTelemetriaAsync(telemetriaExt);
            return Ok(new { status = "success", message = "Datos registrados en Firebase" });
        }

        // POST: /api/Telemetria/ForzarComando
        [HttpPost("ForzarComando")]
        public async Task<IActionResult> ForzarComando([FromBody] string comando)
        {
            if (string.IsNullOrEmpty(comando)) return BadRequest("Comando nulo o inválido.");

            await _firebaseService.SetComandoAsync(comando);
            return Ok(new { status = "success", message = $"Comando '{comando}' inyectado correctamente." });
        }
    }
}
