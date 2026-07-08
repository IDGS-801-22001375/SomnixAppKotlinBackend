using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Somnix.Entities.Rutas;

namespace Somnix.BLL.Services;

public class GoogleMapsService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public GoogleMapsService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["GoogleMaps:ApiKey"] ?? "";
    }

    public async Task<(double distanciaKm, int duracionMinutos, string polyline)> CalcularRuta(PuntoRuta origen, PuntoRuta destino)
    {
        var url = "https://routes.googleapis.com/directions/v2:computeRoutes";

        var body = new
        {
            origin = new
            {
                location = new
                {
                    latLng = new
                    {
                        latitude = origen.Lat,
                        longitude = origen.Lng
                    }
                }
            },
            destination = new
            {
                location = new
                {
                    latLng = new
                    {
                        latitude = destino.Lat,
                        longitude = destino.Lng
                    }
                }
            },
            travelMode = "DRIVE",
            routingPreference = "TRAFFIC_AWARE",
            computeAlternativeRoutes = false,
            languageCode = "es-MX",
            units = "METRIC"
        };

        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Add("X-Goog-Api-Key", _apiKey);
        request.Headers.Add("X-Goog-FieldMask", "routes.duration,routes.distanceMeters,routes.polyline.encodedPolyline");
        request.Content = JsonContent.Create(body);

        var response = await _httpClient.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new Exception($"Error Google Routes API: {json}");

        using var doc = JsonDocument.Parse(json);

        var route = doc.RootElement
            .GetProperty("routes")[0];

        var distanciaMetros = route.GetProperty("distanceMeters").GetDouble();

        var duracionTexto = route.GetProperty("duration").GetString() ?? "0s";
        var duracionSegundos = int.Parse(duracionTexto.Replace("s", ""));

        var polyline = route
            .GetProperty("polyline")
            .GetProperty("encodedPolyline")
            .GetString() ?? "";

        return (
            distanciaKm: Math.Round(distanciaMetros / 1000, 2),
            duracionMinutos: (int)Math.Ceiling(duracionSegundos / 60.0),
            polyline: polyline
        );
    }
}