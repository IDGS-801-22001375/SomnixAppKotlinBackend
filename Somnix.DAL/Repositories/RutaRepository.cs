using Firebase.Database;
using Firebase.Database.Query;
using Somnix.DAL.Interfaces;
using Somnix.Entities.Rutas;

namespace Somnix.DAL.Repositories;

public class RutaRepository : IRutaRepository
{
    private readonly FirebaseClient _firebase;

    public RutaRepository(FirebaseClient firebase)
    {
        _firebase = firebase;
    }

    public async Task<List<Ruta>> ObtenerTodas()
    {
        var rutas = await _firebase
            .Child("somnix")
            .Child("rutas")
            .OnceAsync<Ruta>();

        return rutas.Select(x =>
        {
            x.Object.Id = x.Key;
            return x.Object;
        }).ToList();
    }

    public async Task<Ruta?> ObtenerPorId(string id)
    {
        var ruta = await _firebase
            .Child("somnix")
            .Child("rutas")
            .Child(id)
            .OnceSingleAsync<Ruta>();

        if (ruta == null)
            return null;

        ruta.Id = id;
        return ruta;
    }

    public async Task<Ruta> Crear(Ruta ruta)
    {
        var result = await _firebase
            .Child("somnix")
            .Child("rutas")
            .PostAsync(ruta);

        ruta.Id = result.Key;
        return ruta;
    }

    public async Task<Ruta> Actualizar(string id, Ruta ruta)
    {
        await _firebase
            .Child("somnix")
            .Child("rutas")
            .Child(id)
            .PutAsync(ruta);

        ruta.Id = id;
        return ruta;
    }

    public async Task Eliminar(string id)
    {
        await _firebase
            .Child("somnix")
            .Child("rutas")
            .Child(id)
            .DeleteAsync();
    }
}