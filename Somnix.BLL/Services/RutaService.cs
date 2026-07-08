using Somnix.BLL.Interfaces;
using Somnix.DAL.Interfaces;
using Somnix.DTO.Rutas;
using Somnix.Entities.Rutas;

namespace Somnix.BLL.Services;

public class RutaService : IRutaService
{
    private readonly IRutaRepository _rutaRepository;
    private readonly GoogleMapsService _googleMapsService;

    public RutaService(
        IRutaRepository rutaRepository,
        GoogleMapsService googleMapsService)
    {
        _rutaRepository = rutaRepository;
        _googleMapsService = googleMapsService;
    }

    public async Task<List<RutaResponse>> ObtenerTodas()
    {
        var rutas = await _rutaRepository.ObtenerTodas();
        return rutas.Select(MapearResponse).ToList();
    }

    public async Task<RutaResponse?> ObtenerPorId(string id)
    {
        var ruta = await _rutaRepository.ObtenerPorId(id);

        if (ruta == null)
            return null;

        return MapearResponse(ruta);
    }

    public async Task<RutaResponse> Crear(RutaRequest request)
    {
        var resultadoMapa = await _googleMapsService.CalcularRuta(
            request.Origen,
            request.Destino
        );

        var ruta = new Ruta
        {
            UsuarioId = request.UsuarioId,
            Nombre = request.Nombre,
            Origen = request.Origen,
            Destino = request.Destino,
            DistanciaKm = resultadoMapa.distanciaKm,
            DuracionMinutos = resultadoMapa.duracionMinutos,
            Mapa = new MapaRuta
            {
                Polyline = resultadoMapa.polyline,
                ModoViaje = "DRIVE",
                Proveedor = "Google Maps"
            },
            UbicacionActual = new Coordenada
            {
                Lat = request.Origen.Lat,
                Lng = request.Origen.Lng
            },
            Estado = request.Estado,
            FechaCreacion = DateTime.Now
        };

        var creada = await _rutaRepository.Crear(ruta);
        return MapearResponse(creada);
    }

    public async Task<RutaResponse?> Actualizar(string id, RutaRequest request)
    {
        var rutaExistente = await _rutaRepository.ObtenerPorId(id);

        if (rutaExistente == null)
            return null;

        var resultadoMapa = await _googleMapsService.CalcularRuta(
            request.Origen,
            request.Destino
        );

        rutaExistente.UsuarioId = request.UsuarioId;
        rutaExistente.Nombre = request.Nombre;
        rutaExistente.Origen = request.Origen;
        rutaExistente.Destino = request.Destino;
        rutaExistente.DistanciaKm = resultadoMapa.distanciaKm;
        rutaExistente.DuracionMinutos = resultadoMapa.duracionMinutos;
        rutaExistente.Mapa = new MapaRuta
        {
            Polyline = resultadoMapa.polyline,
            ModoViaje = "DRIVE",
            Proveedor = "Google Maps"
        };
        rutaExistente.UbicacionActual = new Coordenada
        {
            Lat = request.Origen.Lat,
            Lng = request.Origen.Lng
        };
        rutaExistente.Estado = request.Estado;

        var actualizada = await _rutaRepository.Actualizar(id, rutaExistente);
        return MapearResponse(actualizada);
    }

    public async Task<bool> Eliminar(string id)
    {
        var ruta = await _rutaRepository.ObtenerPorId(id);

        if (ruta == null)
            return false;

        await _rutaRepository.Eliminar(id);
        return true;
    }

    private static RutaResponse MapearResponse(Ruta ruta)
    {
        return new RutaResponse
        {
            Id = ruta.Id!,
            UsuarioId = ruta.UsuarioId,
            Nombre = ruta.Nombre,
            Origen = ruta.Origen,
            Destino = ruta.Destino,
            DistanciaKm = ruta.DistanciaKm,
            DuracionMinutos = ruta.DuracionMinutos,
            Mapa = ruta.Mapa,
            UbicacionActual = ruta.UbicacionActual,
            Estado = ruta.Estado,
            FechaCreacion = ruta.FechaCreacion
        };
    }
}