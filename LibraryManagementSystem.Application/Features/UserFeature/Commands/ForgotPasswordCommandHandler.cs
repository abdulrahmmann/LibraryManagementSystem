using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace LibraryManagementSystem.Application.Features.UserFeature.Commands
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, AuthResponse>
    {
        #region Field Instance
        private readonly UserManager<User> _userManager;
        #endregion

        #region Inject Instances Into Constructor
        public ForgotPasswordCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AuthResponse> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.PasswordDTO.Email);

                if (user is null)
                {
                    return new AuthResponse(string.Empty, false, "Invalid Email ", HttpStatusCode.NotFound);
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                if (string.IsNullOrEmpty(token))
                {
                    return new AuthResponse(string.Empty, false, "Token generation failed ", HttpStatusCode.InternalServerError);
                }

                var resetLink = $"https://localhost:5001/api/User/ResetPassword?token={token}&email={request.PasswordDTO.Email}";

                return new AuthResponse(token, user.Email!, true, "Password reset token generated successfully", HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return new AuthResponse(string.Empty, false, ex.Message, HttpStatusCode.InternalServerError);
            }
        }
        #endregion



    }
}
