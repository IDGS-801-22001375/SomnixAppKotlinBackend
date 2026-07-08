using System;
using System.Collections.Generic;
using System.Text;

namespace Somnix.DTO.Auth
{
    public class AuthResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
