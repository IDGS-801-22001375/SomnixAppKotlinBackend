using Somnix.Entities.Rutas;

namespace Somnix.DAL.Interfaces;

public interface IRutaRepository
{
    Task<List<Ruta>> ObtenerTodas();
    Task<Ruta?> ObtenerPorId(string id);
    Task<Ruta> Crear(Ruta ruta);
    Task<Ruta> Actualizar(string id, Ruta ruta);
    Task Eliminar(string id);
}