using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECU.DOMAIN.Entity.Precedure
{
    public class SEL_PersonaResult
    {
        public int IdPersona { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string NumeroIdentificacion { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte TipoIdentificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Ip { get; set; } = string.Empty;  
        public string NumeroIdentificacionCompleto { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
    }
}
