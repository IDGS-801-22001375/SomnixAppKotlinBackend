using Somnix.BLL.Interfaces;
using Somnix.DAL.Interfaces;
using Somnix.DTO.Alertas;
using Somnix.Entities.Alertas;

namespace Somnix.BLL.Services;

public class AlertaService : IAlertaService
{
    private readonly IAlertaRepository _alertaRepository;

    public AlertaService(IAlertaRepository alertaRepository)
    {
        _alertaRepository = alertaRepository;
    }

    public async Task<List<AlertaResponse>> ObtenerTodas()
    {
        var alertas = await _alertaRepository.ObtenerTodas();
        return alertas.Select(MapearResponse).ToList();
    }

    public async Task<List<AlertaResponse>> ObtenerPorUsuario(string usuarioId)
    {
        var alertas = await _alertaRepository.ObtenerPorUsuario(usuarioId);
        return alertas.Select(MapearResponse).ToList();
    }

    public async Task<List<AlertaResponse>> ObtenerPorRuta(string rutaId)
    {
        var alertas = await _alertaRepository.ObtenerPorRuta(rutaId);
        return alertas.Select(MapearResponse).ToList();
    }

    private static AlertaResponse MapearResponse(Alerta alerta)
    {
        return new AlertaResponse
        {
            Id = alerta.Id ?? string.Empty,
            UsuarioId = alerta.UsuarioId,
            RutaId = alerta.RutaId,
            Tipo = alerta.Tipo,
            Nivel = alerta.Nivel,
            Mensaje = alerta.Mensaje,
            Atendida = alerta.Atendida,
            FechaRegistro = alerta.FechaRegistro
        };
    }

    public async Task<bool> MarcarComoLeida(string id)
    {
        return await _alertaRepository.MarcarComoLeida(id);
    }
}