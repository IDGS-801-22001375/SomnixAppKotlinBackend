using Somnix.Entities.Alertas;

namespace Somnix.DAL.Interfaces;

public interface IAlertaRepository
{
    Task<List<Alerta>> ObtenerTodas();
    Task<List<Alerta>> ObtenerPorUsuario(string usuarioId);
    Task<List<Alerta>> ObtenerPorRuta(string rutaId);
    Task<bool> MarcarComoLeida(string id);
}