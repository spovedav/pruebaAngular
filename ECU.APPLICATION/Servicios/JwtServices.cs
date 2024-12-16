using ECU.APPLICATION.Interfaces.Servicios;
using ECU.DOMAIN.DTOs.Autentication;
using ECU.DOMAIN.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECU.APPLICATION.Servicios
{
    public class JwtServices : IJwtServices
    {
        private readonly IConfiguration configuration;
       
        public JwtServices(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public AutenticationResponseDto GenerateToken(AutenticationDto datosAuth)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWT:Key")?.ToString() ?? ""));
            var credenciales = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            int SegundoExpiration = configuration.GetValue<int>("JWT:expireSeg");
            DateTime fechaExpiracion = DateTime.Now.AddSeconds(SegundoExpiration);

            var cliams = new[]
            {
                new Claim(ClaimTypes.Name, datosAuth.UserName),
                new Claim(ClaimTypes.Role, datosAuth.Rol.ToString()),
                new Claim(ClaimTypes.Email, "correo@correo.com"),
                new Claim(ClaimTypes.Expired, fechaExpiracion.ToShortDateString())
            };

            var congiToken = new JwtSecurityToken(
            issuer: configuration.GetValue<string>("JWT:issuer")?.ToString() ?? "",
            audience: configuration.GetValue<string>("JWT:audience")?.ToString() ?? "",
            claims: cliams,
            signingCredentials: credenciales
            );

            var token = new JwtSecurityTokenHandler().WriteToken(congiToken);

            return new AutenticationResponseDto()
            {
                Token = token,
                FechaExpiracion = fechaExpiracion,
                Username = datosAuth.UserName,
                RefreshToken = "pendiente"
            };
        }
    }
}
