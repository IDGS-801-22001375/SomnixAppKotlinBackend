using Firebase.Database;
using Firebase.Database.Query;
using Somnix.DAL.Interfaces;
using Somnix.Entities.Alertas;

namespace Somnix.DAL.Repositories;

public class AlertaRepository : IAlertaRepository
{
    private readonly FirebaseClient _firebase;

    public AlertaRepository(FirebaseClient firebase)
    {
        _firebase = firebase;
    }

    public async Task<List<Alerta>> ObtenerTodas()
    {
        var alertas = await _firebase
            .Child("somnix")
            .Child("alertas")
            .OnceAsync<Alerta>();

        return alertas.Select(x =>
        {
            x.Object.Id = x.Key;
            return x.Object;
        })
        .OrderByDescending(x => x.FechaRegistro)
        .ToList();
    }

    public async Task<List<Alerta>> ObtenerPorUsuario(string usuarioId)
    {
        var alertas = await ObtenerTodas();

        return alertas
            .Where(x => x.UsuarioId == usuarioId)
            .OrderByDescending(x => x.FechaRegistro)
            .ToList();
    }

    public async Task<List<Alerta>> ObtenerPorRuta(string rutaId)
    {
        var alertas = await ObtenerTodas();

        return alertas
            .Where(x => x.RutaId == rutaId)
            .OrderByDescending(x => x.FechaRegistro)
            .ToList();
    }
    public async Task<bool> MarcarComoLeida(string id)
    {
        var alerta = await _firebase
            .Child("somnix")
            .Child("alertas")
            .Child(id)
            .OnceSingleAsync<Alerta>();

        if (alerta == null)
            return false;

        alerta.Id = id;
        alerta.Atendida = true;

        await _firebase
            .Child("somnix")
            .Child("alertas")
            .Child(id)
            .PutAsync(alerta);

        return true;
    }
}