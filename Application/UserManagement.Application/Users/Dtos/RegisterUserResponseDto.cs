namespace UserManagement.Application.Users.Dtos
{
    public record RegisterUserResponseDto(string Username,  string Email, IEnumerable<string> Roles);
}
