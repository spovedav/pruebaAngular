namespace ECU.DOMAIN.DTOs.Autentication
{
    public class AutenticationResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime FechaExpiracion { get; set; }
        public string Username { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
    }
}
