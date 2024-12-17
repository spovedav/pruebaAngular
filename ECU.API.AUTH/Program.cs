using ECU.APPLICATION.Extensiones;
using ECU.APPLICATION.Interfaces.Respositorio;
using ECU.APPLICATION.Interfaces.Servicios;
using ECU.APPLICATION.Servicios;
using ECU.APPLICATION.Utilities;
using ECU.SQL.SERVER.Repositorio;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

UtilititesStartup.CargarDatosIniciales(builder.Configuration);

// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Origen del frontend
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerWithBasicAuth();

builder.Services.AddScoped<IDbConnection>(_ => new SqlConnection(UtilititesStartup.Cadena));

#region REPOSITORIO
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
#endregion

#region SERVICOS
builder.Services.AddScoped<IAutenticaction, Autenticaction>();
builder.Services.AddScoped<IJwtServices, JwtServices>();
#endregion

Log.Logger = new LoggerConfiguration()
           .WriteTo.Console()
           .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
           .CreateLogger();

builder.Services.AddControllersWithViews();
builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        //c.RoutePrefix = string.Empty;  // Opcional: Para que Swagger UI esté en la raíz
    });

}

// Aplicar la política de CORS
app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
