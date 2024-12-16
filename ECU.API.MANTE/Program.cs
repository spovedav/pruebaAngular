using ECU.APPLICATION.Extensiones;
using ECU.APPLICATION.Interfaces.Respositorio;
using ECU.APPLICATION.Interfaces.Servicios;
using ECU.APPLICATION.Servicios;
using ECU.APPLICATION.Utilities;
using ECU.SQL.SERVER.Repositorio;
using Microsoft.Data.SqlClient;
using Serilog;
using System.Data;

try
{

    var builder = WebApplication.CreateBuilder(args);

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

    // Add services to the container.
    builder.Services.AddSwaggerWithBearerToken(builder.Configuration);

    UtilititesStartup.CargarDatosIniciales(builder.Configuration);

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddScoped<IDbConnection>(_ => new SqlConnection(UtilititesStartup.Cadena));

    #region REPOSITORI
    builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
    builder.Services.AddScoped<IPersonaRepositorio, PersonaRepositorio>();
    #endregion

    #region SERVICES
    builder.Services.AddScoped<IPersonaService, PersonaService>();
    builder.Services.AddScoped<IUsuarioService, UsuarioService>();
    #endregion

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "CAPA API ADMIN");
            //options.RoutePrefix = string.Empty;
        });
    }

    // Aplicar la política de CORS
    app.UseCors("AllowAllOrigins");

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    Log.Error("Program", ex);
}
