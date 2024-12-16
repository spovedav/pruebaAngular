using ECU.APPLICATION.Interfaces.Respositorio;
using ECU.APPLICATION.Interfaces.Servicios;
using ECU.DOMAIN.Constante;
using ECU.DOMAIN.DTOs;
using ECU.DOMAIN.DTOs.Autentication;
using Serilog;
using System.Text;

namespace ECU.APPLICATION.Servicios
{
    public class Autenticaction : IAutenticaction
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IJwtServices _jwtServices;

        public Autenticaction(IUsuarioRepositorio _usuarioRepositorio, IJwtServices _jwtServices)
        {
            this._usuarioRepositorio = _usuarioRepositorio;
            this._jwtServices = _jwtServices;
        }

        public async Task<ResultResponse<AutenticationResponseDto>> AutenticarLoguin(string authorizationHeader)
        {
            var response = new ResultResponse<AutenticationResponseDto>();

            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Basic "))
                return response.Failed("Falta el encabezado de autorización o no es válido.");

            try
            {
                var encodedCredentials = authorizationHeader.Substring("Basic ".Length).Trim();
                var decodedCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCredentials));
                var credentials = decodedCredentials.Split(':');

                if (credentials.Length != 2)
                    return response.Failed("Formato de credenciales no válido.");

                var username = credentials.FirstOrDefault();
                var password = credentials.LastOrDefault();

                var usuarioData = await _usuarioRepositorio.GetUsuario(username ?? "", password??"");

                if (usuarioData == null || string.IsNullOrEmpty(usuarioData?.Usuario))
                    return response.Failed("Nombre de usuario o contraseña no válidos.");

                var resultToken = _jwtServices.GenerateToken(new AutenticationDto(usuarioData));
                response.Result = resultToken;
                return response;
            }
            catch (OperationCanceledException ex)
            {
                Log.Error("Operación cancelada");
                return response.Failed("Operation was canceled");
            }
            catch (Exception ex)
            {
                Log.Error("Exception en Autenticar", ex);
                return response.Failed("Se produjo un error al procesar el encabezado de autorización.");
            }

        }
    }
}
