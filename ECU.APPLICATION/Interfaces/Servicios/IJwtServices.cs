using ECU.DOMAIN.DTOs.Autentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECU.APPLICATION.Interfaces.Servicios
{
    public interface IJwtServices
    {
        AutenticationResponseDto GenerateToken(AutenticationDto datosAuth);
    }
}
