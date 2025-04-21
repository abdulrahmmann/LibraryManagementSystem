using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace LibraryManagementSystem.Application.Features.UserFeature.Commands
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, AuthResponse>
    {
        #region Field Instance
        private readonly UserManager<User> _userManager;
        #endregion

        #region Inject Instance Into Constructor
        public ResetPasswordCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        #endregion

        public async Task<AuthResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.PasswordDTO.Email);

                if (user == null)
                {
                    return new AuthResponse(string.Empty, false, "User Not Found!", HttpStatusCode.NotFound);
                }

                var result = await _userManager.ResetPasswordAsync(user!, request.PasswordDTO.Token, request.PasswordDTO.Email);

                if (result.Succeeded)
                {
                    return new AuthResponse(string.Empty, true, "Password Reset Success!", HttpStatusCode.OK);
                }

                return new AuthResponse(string.Empty, false, "Password Reset Failed!", HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                return new AuthResponse(string.Empty, false, ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
