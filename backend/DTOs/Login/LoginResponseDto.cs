namespace backend.DTOs.Login
{
    public class LoginResponseDto
    {
        public string JwtToken { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
