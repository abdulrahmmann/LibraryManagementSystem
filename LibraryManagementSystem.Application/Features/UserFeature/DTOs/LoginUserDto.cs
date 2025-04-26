namespace LibraryManagementSystem.Application.Features.UserFeature.DTOs
{
    public sealed class LoginUserDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
