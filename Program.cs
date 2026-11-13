using Microsoft.EntityFrameworkCore;
using DSW_3P_PR02.Data;
using DSW_3P_PR02.Services;
using DSW_3P_PR02.Repositories;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

//Agregar controladores
builder.Services.AddControllers();

//Agregar dependencias
builder.Services.AddScoped<IEventoRepository, EventoSQLRepository>();
builder.Services.AddScoped<IEventoService, EventoService>();
//Configuración del SQL SERVER
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SqlServerConnection")
    )
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            "/openapi/v1.json",
            "API de Eventos v1"
        );
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();