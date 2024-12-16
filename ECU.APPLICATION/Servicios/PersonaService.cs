using ECU.APPLICATION.Interfaces.Respositorio;
using ECU.APPLICATION.Interfaces.Servicios;
using ECU.DOMAIN.DTOs;
using ECU.DOMAIN.DTOs.Persona;

namespace ECU.APPLICATION.Servicios
{
    public class PersonaService : IPersonaService
    {
        private readonly IPersonaRepositorio repositorio;

        public PersonaService(IPersonaRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public ResultResponse<bool> Create(PersonaDto request)
        {
            var response = new ResultResponse<bool>();

            var resul = repositorio.Create(request);

            if (resul.Error)
                return response.Failed(response.Mensaje);

            response.Result = true;
            response.Mensaje = resul.Mensaje;

            return response;
        }

        public ResultResponse<bool> Delete(int IdPersona)
        {
            var response = new ResultResponse<bool>();

            var resul = repositorio.Delete(IdPersona);

            if (resul.Error)
                return response.Failed(response.Mensaje);

            response.Result = true;
            response.Mensaje = resul.Mensaje;

            return response;
        }

        public async Task<ResultResponse<PersonaDto>> Get(int IdPersona)
        {
            var response = new ResultResponse<PersonaDto>();

            var resul = await repositorio.Get(IdPersona);

            response.Result = new PersonaDto(resul);
            response.Mensaje = (resul is null || string.IsNullOrEmpty(resul.NumeroIdentificacion)) ? "Consulta Vacía" : "";

            return response;
        }

        public async Task<ResultResponse<List<PersonaDto>>> GetAll()
        {
            var response = new ResultResponse<List<PersonaDto>>();

            var resul = await repositorio.GetAll();

            response.Result = resul.Select(x=> new PersonaDto(x)).ToList();
            response.Mensaje = !resul.Any() ? "Consulta Vacía" : "";

            return response;
        }

        public ResultResponse<bool> Update(PersonaDto request)
        {
            var response = new ResultResponse<bool>();

            var resul = repositorio.Update(request);

            if (resul.Error)
                return response.Failed(response.Mensaje);

            response.Result = true;
            response.Mensaje = resul.Mensaje;

            return response;
        }
    }
}
