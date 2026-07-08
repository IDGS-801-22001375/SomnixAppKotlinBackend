using Somnix.DTO.Auth;
using Somnix.DTO.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace Somnix.BLL.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResponse> Registrar(UsuarioRegistroRequest request);
        Task<AuthResponse?> Login(LoginRequest request);
        Task<AuthResponse> LoginGoogle(GoogleLoginRequest request);
    }
}
