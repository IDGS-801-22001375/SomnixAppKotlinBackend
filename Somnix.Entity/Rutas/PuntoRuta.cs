using System;
using System.Collections.Generic;
using System.Text;

namespace Somnix.Entities.Rutas
{
    public class PuntoRuta
    {
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string PlaceId { get; set; } = string.Empty;
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
