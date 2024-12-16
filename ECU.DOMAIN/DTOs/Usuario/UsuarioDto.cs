using ECU.DOMAIN.Entity.Precedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECU.DOMAIN.DTOs.Usuario
{
    public class UsuarioDto
    {
        public UsuarioDto()
        {
            
        }

        public UsuarioDto(SEL_UsuarioResult iten)
        {
            this.IdUsuario = iten.IdUsuario;
            this.Identificador = iten.Identificador;
            this.Usuario = iten.Usuario;
            this.Rol = iten.Rol;
            this.FechaCreacion = iten.FechaCreacion;
            this.Ip = iten.Ip;
        }

        public int IdUsuario { get; set; }
        public string Identificador { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public byte Rol { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Ip { get; set; } = string.Empty;
    }
}
