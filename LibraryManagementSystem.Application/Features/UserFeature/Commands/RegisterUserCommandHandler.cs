using System.Globalization;
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
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, AuthResponse>
    {
        #region Field Instance
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        #endregion

        #region Inject Instances Into Constructor
        public RegisterUserCommandHandler(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        #endregion


        public async Task<AuthResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userEmailExist = await _userManager.FindByEmailAsync(request.UserDTO.Email);

                var userNameExist = await _userManager.FindByNameAsync(request.UserDTO.UserName);

                if (userEmailExist != null)
                {
                    return new AuthResponse(string.Empty, false, "Email Is Already Exist!", HttpStatusCode.Conflict);
                }

                if (userNameExist != null)
                {
                    return new AuthResponse(string.Empty, false, "UserName Is Already Exist!", HttpStatusCode.Conflict);
                }

                var newUser = new User
                {
                    Email = request.UserDTO.Email,
                    UserName = request.UserDTO.UserName,
                };

                var isCreated = await _userManager.CreateAsync(newUser, request.UserDTO.Password);

                if (isCreated.Succeeded)
                {
                    // Generate JWT Token
                    var generatedToken = GenerateJwtToken(newUser);

                    return new AuthResponse(generatedToken, true, "User Registration Success!", HttpStatusCode.OK);
                }

                return new AuthResponse(string.Empty, false, "User Registration Failed!", HttpStatusCode.InternalServerError);

            }
            catch (Exception ex)
            {
                return new AuthResponse(string.Empty, false, ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        private string GenerateJwtToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? throw new InvalidOperationException());

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity([
                    new Claim("Id", user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture))
                ]),

                Expires = DateTime.UtcNow.AddDays(7),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256),
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var jwtToken = jwtTokenHandler.WriteToken(token);


            return jwtToken;
        }
    }
}