using Somnix.DTO.Rutas;

namespace Somnix.BLL.Interfaces;

public interface IRutaService
{
    Task<List<RutaResponse>> ObtenerTodas();
    Task<RutaResponse?> ObtenerPorId(string id);
    Task<RutaResponse> Crear(RutaRequest request);
    Task<RutaResponse?> Actualizar(string id, RutaRequest request);
    Task<bool> Eliminar(string id);
}