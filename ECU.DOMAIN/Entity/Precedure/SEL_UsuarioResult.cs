using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECU.DOMAIN.Entity.Precedure
{
    public class SEL_UsuarioResult
    {
        public int IdUsuario { get; set; }
        public string Identificador { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public byte Rol { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Ip { get; set; } = string.Empty;
    }
}
