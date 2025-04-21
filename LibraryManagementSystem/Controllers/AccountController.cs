using LibraryManagementSystem.Application.Features.UserFeature.Commands;
using LibraryManagementSystem.Application.Features.UserFeature.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO userDTO)
        {
            var result = await _mediator.Send(new RegisterUserCommand(userDTO));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(result);
        }
    }
}
