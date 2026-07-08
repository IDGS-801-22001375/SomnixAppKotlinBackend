using Firebase.Database;
using Firebase.Database.Query;
using Somnix.DAL.Interfaces;
using Somnix.Entities.Usuarios;

namespace Somnix.DAL.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly FirebaseClient _firebase;

    public UsuarioRepository(FirebaseClient firebase)
    {
        _firebase = firebase;
    }

    public async Task<Usuario?> ObtenerPorEmail(string email)
    {
        var usuarios = await _firebase
            .Child("somnix")
            .Child("usuarios")
            .OnceAsync<Usuario>();

        var usuario = usuarios.FirstOrDefault(x =>
            x.Object.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        if (usuario == null)
            return null;

        usuario.Object.Id = usuario.Key;
        return usuario.Object;
    }

    public async Task<Usuario?> ObtenerPorId(string id)
    {
        var usuario = await _firebase
            .Child("somnix")
            .Child("usuarios")
            .Child(id)
            .OnceSingleAsync<Usuario>();

        if (usuario == null)
            return null;

        usuario.Id = id;
        return usuario;
    }

    public async Task<Usuario> Registrar(Usuario usuario)
    {
        var result = await _firebase
            .Child("somnix")
            .Child("usuarios")
            .PostAsync(usuario);

        usuario.Id = result.Key;
        return usuario;
    }

    public async Task<Usuario> Actualizar(string id, Usuario usuario)
    {
        await _firebase
            .Child("somnix")
            .Child("usuarios")
            .Child(id)
            .PutAsync(usuario);

        usuario.Id = id;
        return usuario;
    }
}