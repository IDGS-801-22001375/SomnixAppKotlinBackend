using Somnix.DTO.Alertas;

namespace Somnix.BLL.Interfaces;

public interface IAlertaService
{
    Task<List<AlertaResponse>> ObtenerTodas();
    Task<List<AlertaResponse>> ObtenerPorUsuario(string usuarioId);
    Task<List<AlertaResponse>> ObtenerPorRuta(string rutaId);
    Task<bool> MarcarComoLeida(string id);
}