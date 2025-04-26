using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.UserFeature.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.UserFeature.Commands
{
    public record ForgotPasswordCommand(ForgotPasswordDto PasswordDTO) : IRequest<AuthResponse>;
    public record ResetPasswordCommand(ResetPasswordDto PasswordDTO) : IRequest<AuthResponse>;
}
