using ECU.APPLICATION.Interfaces.Servicios;
using ECU.DOMAIN.DTOs.Persona;
using ECU.DOMAIN.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECU.DOMAIN.DTOs.Usuario;

namespace ECU.API.MANTE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService service;

        public UsuarioController(IUsuarioService service)
        {
            this.service = service;
        }

        [HttpGet("list")]
        public async Task<ActionResult<ResultResponse<List<UsuarioDto>>>> Lista()
            => Ok(await service.GetAll());

        [HttpGet("{id}")]
        public async Task<ActionResult<ResultResponse<PersonaDto>>> Get(int id)
                   => Ok(await service.Get(id));

        [HttpPost("create")]
        public ActionResult<ResultResponse<bool>> Create(UsuarioDto request)
            => Ok(service.Create(request));

        [HttpPut("update")]
        public ActionResult<ResultResponse<bool>> Update(UsuarioDto request)
            => Ok(service.Update(request));

        [HttpDelete("{id}")]
        public ActionResult<ResultResponse<bool>> Delete(int id)
            => Ok(service.Delete(id));

    }
}
