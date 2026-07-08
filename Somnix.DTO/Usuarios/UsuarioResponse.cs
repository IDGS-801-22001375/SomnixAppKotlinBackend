using System;
using System.Collections.Generic;
using System.Text;

namespace Somnix.DTO.Usuarios
{
    public class UsuarioResponse
    {
        public string? Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
    }
}
