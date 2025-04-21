using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.UserFeature.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.UserFeature.Commands
{
    public record ForgotPasswordCommand(ForgotPasswordDTO PasswordDTO) : IRequest<AuthResponse>;
    public record ResetPasswordCommand(ResetPasswordDTO PasswordDTO) : IRequest<AuthResponse>;
}
