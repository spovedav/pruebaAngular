using ECU.DOMAIN.Entity.Precedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECU.DOMAIN.DTOs.Persona
{
    public class PersonaDto
    {
        public PersonaDto()
        {
            
        }

        public PersonaDto(SEL_PersonaResult iten)
        {
            this.IdPersona = iten.IdPersona;
            this.Nombres = iten.Nombres;
            this.NumeroIdentificacion = iten.NumeroIdentificacion;
            this.Apellidos = iten.Apellidos;
            this.Email = iten.Email;
            this.TipoIdentificacion = iten.TipoIdentificacion;
            this.Ip = iten.Ip;
        }

        public int IdPersona { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string NumeroIdentificacion { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte TipoIdentificacion { get; set; }
        public string Ip { get; set; } = string.Empty;
    }
}
