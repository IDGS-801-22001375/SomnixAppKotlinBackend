using Somnix.Entities.Rutas;
using System;
using System.Collections.Generic;
using System.Text;

namespace Somnix.DTO.Rutas
{
    public class RutaRequest
    {
        public string UsuarioId { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;

        public PuntoRuta Origen { get; set; } = new();
        public PuntoRuta Destino { get; set; } = new();

        public string Estado { get; set; } = "pendiente";
    }
}
