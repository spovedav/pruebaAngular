using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace ECU.APPLICATION.Extensiones
{
    public static class SwaggerExtension
    {
        public static void AddSwaggerWithBasicAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // Define el esquema de seguridad para Basic Authentication
                c.AddSecurityDefinition("BasicAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    Description = "Introduzca su nombre de usuario y contraseña en el formato 'nombre de usuario:contraseña'.\r\n"
                });

                // Aplica el esquema de seguridad a todas las operaciones de la API
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "BasicAuth"
                            }
                        },
                        new string[] { }
                    }
                });
            });
        }

        public static void AddSwaggerWithBearerToken(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = false,// esto para que es
                       ValidateIssuerSigningKey = true,
                       ValidAudience = configuration.GetValue<string>("JWT:audience")?.ToString() ?? "",
                       ValidIssuer = configuration.GetValue<string>("JWT:issuer")?.ToString() ?? "",
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWT:Key")?.ToString() ?? ""))
                   };
               });

            services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
            });
        }
    }
}
