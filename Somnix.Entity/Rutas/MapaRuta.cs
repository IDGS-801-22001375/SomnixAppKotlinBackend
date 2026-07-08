using System;
using System.Collections.Generic;
using System.Text;

namespace Somnix.Entities.Rutas
{
    public class MapaRuta
    {
        public string Polyline { get; set; } = string.Empty;
        public string ModoViaje { get; set; } = "DRIVE";
        public string Proveedor { get; set; } = "Google Maps";
    }
}
