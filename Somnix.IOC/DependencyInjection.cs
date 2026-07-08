using Firebase.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sominix.Common;
using Somnix.BLL.Interfaces;
using Somnix.BLL.Services;
using Somnix.DAL.Interfaces;
using Somnix.DAL.Repositories;

namespace Somnix.IOC;

public static class DependencyInjection
{
    public static IServiceCollection AddSomnixServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var firebaseUrl = configuration["FirebaseSettings:DatabaseUrl"];

        if (string.IsNullOrWhiteSpace(firebaseUrl))
            throw new Exception("FirebaseSettings:DatabaseUrl no está configurado en appsettings.json.");

        services.AddSingleton(new FirebaseClient(firebaseUrl));

        services.Configure<JwtSettings>(
            configuration.GetSection("JwtSettings"));

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();

        services.AddScoped<IUsuarioService, UsuarioService>();

        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IRutaRepository, RutaRepository>();

        services.AddScoped<IRutaService, RutaService>();

        services.AddScoped<IAlertaRepository, AlertaRepository>();

        services.AddScoped<IAlertaService, AlertaService>();

        services.AddSingleton<FirebaseService>();

        return services;
    }
}