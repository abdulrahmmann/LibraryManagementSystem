using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace LibraryManagementSystem.Application.Features.UserFeature.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResponse>
    {
        #region Field Instance
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        #endregion

        #region Inject Instances Into Constructor
        public LoginUserCommandHandler(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        #endregion

        public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var existingUser = await _userManager.FindByEmailAsync(request.UserDTO.Email);

                if (existingUser is null)
                {
                    return new AuthResponse(string.Empty, false, "user not found! ", HttpStatusCode.NotFound);
                }

                var isPasswordValid = await _userManager.CheckPasswordAsync(existingUser, request.UserDTO.Password);

                if (!isPasswordValid)
                {
                    return new AuthResponse(string.Empty, false, "Invalid password! ", HttpStatusCode.Unauthorized);
                }

                var generatedToken = GenerateJwtToken(existingUser);

                return new AuthResponse(generatedToken, true, "User Login Success!", HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                return new AuthResponse(string.Empty, false, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        private string GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                }),

                Expires = DateTime.UtcNow.AddDays(7),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var jwtToken = jwtTokenHandler.WriteToken(token);


            return jwtToken;
        }
    }
}
