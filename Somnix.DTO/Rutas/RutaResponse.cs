using Somnix.Entities.Rutas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Somnix.DTO.Rutas
{
    public class RutaResponse
    {
        public string Id { get; set; } = string.Empty;
        public string UsuarioId { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;

        public PuntoRuta Origen { get; set; } = new();
        public PuntoRuta Destino { get; set; } = new();

        public double DistanciaKm { get; set; }
        public int DuracionMinutos { get; set; }

        public MapaRuta Mapa { get; set; } = new();
        public Coordenada UbicacionActual { get; set; } = new();

        public string Estado { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }
}
