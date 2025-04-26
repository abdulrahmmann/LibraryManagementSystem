namespace LibraryManagementSystem.Application.Features.UserFeature.DTOs
{
    public sealed class ResetPasswordDto
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
