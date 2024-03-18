namespace UserManagement.Application.Users.Dtos
{
    public class LoginResponseDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
    }
}
