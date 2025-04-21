namespace LibraryManagementSystem.Application.Features.UserFeature.DTOs
{
    public class ResetPasswordDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
