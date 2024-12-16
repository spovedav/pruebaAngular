using ECU.APPLICATION.Interfaces.Respositorio;
using ECU.APPLICATION.Interfaces.Servicios;
using ECU.DOMAIN.DTOs;
using ECU.DOMAIN.DTOs.Usuario;

namespace ECU.APPLICATION.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepositorio repositorio;

        public UsuarioService(IUsuarioRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        public ResultResponse<bool> Create(UsuarioDto request)
        {
            var response = new ResultResponse<bool>();

            var resul = repositorio.Create(request);

            if (resul.Error)
                return response.Failed(response.Mensaje);

            response.Result = true;
            response.Mensaje = resul.Mensaje;

            return response;
        }

        public ResultResponse<bool> Delete(int IdUsuario)
        {
            var response = new ResultResponse<bool>();

            var resul = repositorio.Delete(IdUsuario);

            if (resul.Error)
                return response.Failed(response.Mensaje);

            response.Result = true;
            response.Mensaje = resul.Mensaje;

            return response;
        }

        public async Task<ResultResponse<UsuarioDto>> Get(int IdUsuario)
        {
            var response = new ResultResponse<UsuarioDto>();

            var resul = await repositorio.Get(IdUsuario);

            response.Result = new UsuarioDto(resul);
            response.Mensaje = (resul is null || string.IsNullOrEmpty(resul.Identificador)) ? "Consulta Vacía" : "";

            return response;
        }

        public async Task<ResultResponse<List<UsuarioDto>>> GetAll()
        {
            var response = new ResultResponse<List<UsuarioDto>>();

            var resul = await repositorio.GetAll();

            response.Result =  resul.Select(x=> new UsuarioDto(x)).ToList();
            response.Mensaje = !resul.Any() ? "Consulta Vacía" : "";

            return response;
        }

        public ResultResponse<bool> Update(UsuarioDto request)
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
