using System;
using System.Collections.Generic;
using System.Text;

namespace Somnix.Entities.Rutas
{
    public class Ruta
    {
        public string? Id { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;

        public PuntoRuta Origen { get; set; } = new();
        public PuntoRuta Destino { get; set; } = new();

        public double DistanciaKm { get; set; }
        public int DuracionMinutos { get; set; }

        public MapaRuta Mapa { get; set; } = new();

        public Coordenada UbicacionActual { get; set; } = new();

        public Dictionary<string, Coordenada> Recorrido { get; set; } = new();

        public string Estado { get; set; } = "pendiente";
        public DateTime FechaCreacion { get; set; }
    }
}
