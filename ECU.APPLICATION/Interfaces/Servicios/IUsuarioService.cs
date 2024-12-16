using ECU.DOMAIN.DTOs;
using ECU.DOMAIN.DTOs.Usuario;
using ECU.DOMAIN.Entity.Precedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECU.APPLICATION.Interfaces.Servicios
{
    public interface IUsuarioService
    {
        Task<ResultResponse<List<UsuarioDto>>> GetAll();
        Task<ResultResponse<UsuarioDto>> Get(int IdUsuario);
        ResultResponse<bool> Create(UsuarioDto request);
        ResultResponse<bool> Update(UsuarioDto request);
        ResultResponse<bool> Delete(int IdUsuario);
    }
}
