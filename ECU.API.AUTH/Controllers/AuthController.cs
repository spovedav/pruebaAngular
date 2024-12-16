using ECU.APPLICATION.Interfaces.Servicios;
using ECU.DOMAIN.Constante;
using ECU.DOMAIN.DTOs;
using ECU.DOMAIN.DTOs.Autentication;
using Microsoft.AspNetCore.Mvc;

namespace ECU.API.AUTH.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAutenticaction auth;

        public AuthController(IAutenticaction auth)
        {
            this.auth = auth;
        }

        [HttpGet]
        public async Task<ActionResult<ResultResponse<AutenticationResponseDto>>> Get(CancellationToken cancellationToken)
            => await auth.AutenticarLoguin(Request.Headers["Authorization"].ToString());
    }
}
