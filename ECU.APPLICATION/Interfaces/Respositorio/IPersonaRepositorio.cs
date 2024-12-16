using ECU.DOMAIN.DTOs.Persona;
using ECU.DOMAIN.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECU.DOMAIN.Entity.Precedure;

namespace ECU.APPLICATION.Interfaces.Respositorio
{
    public interface IPersonaRepositorio
    {
        Task<List<SEL_PersonaResult>> GetAll();
        Task<SEL_PersonaResult> Get(int IdPersona);
        SEL_Result Create(PersonaDto request);
        SEL_Result Update(PersonaDto request);
        SEL_Result Delete(int IdPersona);
    }
}
