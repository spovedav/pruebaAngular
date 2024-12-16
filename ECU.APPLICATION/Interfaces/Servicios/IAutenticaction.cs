using ECU.DOMAIN.Constante;
using ECU.DOMAIN.DTOs;
using ECU.DOMAIN.DTOs.Autentication;

namespace ECU.APPLICATION.Interfaces.Servicios
{
    public interface IAutenticaction
    {
        Task<ResultResponse<AutenticationResponseDto>> AutenticarLoguin(string authorizationHeader);
    }
}
