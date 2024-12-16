using ECU.APPLICATION.Interfaces.Servicios;
using ECU.DOMAIN.DTOs;
using ECU.DOMAIN.DTOs.Persona;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECU.API.MANTE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly IPersonaService service;

        public PersonaController(IPersonaService service) { this.service = service; }

        [HttpGet("list")]
        public async Task<ActionResult<ResultResponse<List<PersonaDto>>>> Lista()
            => Ok(await service.GetAll());

        [HttpGet("{id}")]
        public async Task<ActionResult<ResultResponse<PersonaDto>>> Get(int id)
            => Ok(await service.Get(id));

        [HttpPost("create")]
        public ActionResult<ResultResponse<bool>> Create(PersonaDto request)
            => Ok(service.Create(request));

        [HttpPut("update")]
        public ActionResult<ResultResponse<bool>> Update(PersonaDto request)
            => Ok(service.Update(request));

        [HttpDelete("{id}")]
        public ActionResult<ResultResponse<bool>> Delete(int id)
            => Ok(service.Delete(id));
    }

}
