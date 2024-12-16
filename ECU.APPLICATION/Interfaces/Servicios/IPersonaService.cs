using ECU.DOMAIN.DTOs;
using ECU.DOMAIN.DTOs.Persona;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECU.APPLICATION.Interfaces.Servicios
{
    public interface IPersonaService
    {
        Task<ResultResponse<List<PersonaDto>>> GetAll();
        Task<ResultResponse<PersonaDto>> Get(int IdPersona);
        ResultResponse<bool> Create(PersonaDto request);
        ResultResponse<bool> Update(PersonaDto request);
        ResultResponse<bool> Delete(int IdPersona);
    }
}
