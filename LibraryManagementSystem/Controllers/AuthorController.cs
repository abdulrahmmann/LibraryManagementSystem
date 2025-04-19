using LibraryManagementSystem.Application.Features.AuthorFeature.Commands;
using LibraryManagementSystem.Application.Features.AuthorFeature.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Controllers
{
    public class AuthorController : AppControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDTO authorDTO)
        {
            var result = await Mediator.Send(new CreateAuthorCommand(authorDTO));

            return NewResult(result);
        }

        [HttpPost("createRange")]
        public async Task<IActionResult> CreateAuthorRange([FromBody] IEnumerable<AuthorDTO> authorDTOs)
        {
            var result = await Mediator.Send(new CreateAuthorRangeCommand(authorDTOs));

            return NewResult(result);
        }
    }
}