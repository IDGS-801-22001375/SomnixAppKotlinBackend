using Somnix.BLL.Interfaces;
using Somnix.DAL.Interfaces;
using Somnix.DTO.Auth;
using Somnix.DTO.Usuarios;
using Somnix.Entities.Usuarios;
using Google.Apis.Auth;

namespace Somnix.BLL.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenService _tokenService;

    public UsuarioService(
        IUsuarioRepository usuarioRepository,
        ITokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _tokenService = tokenService;
    }

    public async Task<UsuarioResponse> Registrar(UsuarioRegistroRequest request)
    {
        ValidarRegistro(request);

        var existe = await _usuarioRepository.ObtenerPorEmail(request.Email);

        if (existe != null)
            throw new Exception("El correo ya se encuentra registrado.");

        var usuario = new Usuario
        {
            Nombre = request.Nombre,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Rol = "usuario",
            Estatus = true,
            FechaRegistro = DateTime.Now
        };

        var creado = await _usuarioRepository.Registrar(usuario);

        return new UsuarioResponse
        {
            Id = creado.Id,
            Nombre = creado.Nombre,
            Email = creado.Email,
            FechaRegistro = creado.FechaRegistro
        };
    }

    public async Task<AuthResponse?> Login(LoginRequest request)
    {
        ValidarLogin(request);

        var usuario = await _usuarioRepository.ObtenerPorEmail(request.Email);

        if (usuario == null)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash))
            return null;

        usuario.UltimoLogin = DateTime.Now;

        await _usuarioRepository.Actualizar(usuario.Id!, usuario);

        var token = _tokenService.GenerarToken(usuario);

        return new AuthResponse
        {
            Id = usuario.Id!,
            Nombre = usuario.Nombre,
            Email = usuario.Email,
            Rol = usuario.Rol,
            Token = token
        };
    }

    public async Task<AuthResponse> LoginGoogle(GoogleLoginRequest request)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);

        var usuario = await _usuarioRepository.ObtenerPorEmail(payload.Email);

        if (usuario == null)
        {
            usuario = new Usuario
            {
                Nombre = payload.Name ?? payload.Email,
                Email = payload.Email,
                PasswordHash = "",
                Rol = "usuario",
                Estatus = true,
                FechaRegistro = DateTime.Now,
                UltimoLogin = DateTime.Now
            };

            usuario = await _usuarioRepository.Registrar(usuario);
        }
        else
        {
            usuario.UltimoLogin = DateTime.Now;
            await _usuarioRepository.Actualizar(usuario.Id!, usuario);
        }

        var token = _tokenService.GenerarToken(usuario);

        return new AuthResponse
        {
            Id = usuario.Id!,
            Nombre = usuario.Nombre,
            Email = usuario.Email,
            Rol = usuario.Rol,
            Token = token
        };
    }

    private void ValidarRegistro(UsuarioRegistroRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nombre))
            throw new Exception("El nombre es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.Email))
            throw new Exception("El correo es obligatorio.");

        if (!request.Email.Contains("@"))
            throw new Exception("El correo no tiene un formato válido.");

        if (string.IsNullOrWhiteSpace(request.Password))
            throw new Exception("La contraseña es obligatoria.");

        if (request.Password.Length < 6)
            throw new Exception("La contraseña debe tener al menos 6 caracteres.");
    }

    private void ValidarLogin(LoginRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new Exception("El correo es obligatorio.");

        if (!request.Email.Contains("@"))
            throw new Exception("El correo no tiene un formato válido.");

        if (string.IsNullOrWhiteSpace(request.Password))
            throw new Exception("La contraseña es obligatoria.");
    }
}