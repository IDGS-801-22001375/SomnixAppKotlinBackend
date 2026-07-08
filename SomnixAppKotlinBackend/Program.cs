using Somnix.BLL.Services;
using Somnix.IOC;

var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddControllers();
builder.Services.AddHttpClient<GoogleMapsService>();

builder.Services.AddOpenApi();

// Inyección de dependencias
builder.Services.AddSomnixServices(builder.Configuration);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("SomnixPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5130);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Middleware
//app.UseHttpsRedirection();

app.UseCors("SomnixPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();