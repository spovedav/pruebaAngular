using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECU.DOMAIN.Entity.Precedure
{
    public class SEL_UsuarioAutenticationResult
    {
        public string Identificacion { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public byte Rol { get; set; }
    }
}
