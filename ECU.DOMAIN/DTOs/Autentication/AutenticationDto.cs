using ECU.DOMAIN.Entity.Precedure;

namespace ECU.DOMAIN.DTOs.Autentication
{
    public sealed class AutenticationDto
    {
        public AutenticationDto(SEL_UsuarioAutenticationResult datos)
        {
            this.UserName = datos.Usuario;
            this.Rol = datos.Rol;
        }

        public AutenticationDto(string UserName, byte Rol)
        {
            this.UserName = UserName;
            this.Rol = Rol;
        }

        public string UserName { get; init; }
        public byte Rol { get; init; }
    }
}
