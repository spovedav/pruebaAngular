using ECU.DOMAIN.DTOs.Persona;
using ECU.DOMAIN.DTOs.Usuario;
using ECU.DOMAIN.Entity.Precedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECU.APPLICATION.Interfaces.Respositorio
{
    public interface IUsuarioRepositorio
    {
        Task<SEL_UsuarioAutenticationResult> GetUsuario(string UserName, string Passs);

        Task<List<SEL_UsuarioResult>> GetAll();
        Task<SEL_UsuarioResult> Get(int IdUsuario);
        SEL_Result Create(UsuarioDto request);
        SEL_Result Update(UsuarioDto request);
        SEL_Result Delete(int IdUsuario);
    }
}
