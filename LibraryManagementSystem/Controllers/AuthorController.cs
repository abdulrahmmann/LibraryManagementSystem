using LibraryManagementSystem.Application.Features.AuthorFeature.Commands;
using LibraryManagementSystem.Application.Features.AuthorFeature.DTOs;
using LibraryManagementSystem.Application.Features.AuthorFeature.Queries;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Controllers
{
    public class AuthorController : AppControllerBase
    {
        [HttpPost("createAuthor")]
        public async Task<IActionResult> CreateAuthor([FromBody] AuthorDTO authorDTO)
        {
            var result = await Mediator.Send(new CreateAuthorCommand(authorDTO));

            return NewResult(result);
        }

        [HttpPost("createAuthorRange")]
        public async Task<IActionResult> CreateAuthorRange([FromBody] IEnumerable<AuthorDTO> authorDTOs)
        {
            var result = await Mediator.Send(new CreateAuthorRangeCommand(authorDTOs));

            return NewResult(result);
        }

        [HttpGet("GetAllAuthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var result = await Mediator.Send(new GetAllAuthorsQuery());

            return NewResult(result);
        }

        [HttpGet("GetAuthorById")]
        public async Task<IActionResult> GetAuthorById(int Id)
        {
            var result = await Mediator.Send(new GetAuthorByIdQuery(Id));

            return NewResult(result);
        }

        [HttpGet("GetAuthorByName")]
        public async Task<IActionResult> GetAuthorByName(string Name)
        {
            var result = await Mediator.Send(new GetAuthorByNameQuery(Name));

            return NewResult(result);
        }

        [HttpGet("FilterAuthorByName")]
        public async Task<IActionResult> FilterAuthorByName(string Name)
        {
            var result = await Mediator.Send(new FilterAuthorByNameQuery(Name));

            return NewResult(result);
        }

        [HttpGet("GetAuthorByNationality")]
        public async Task<IActionResult> GetAuthorByNationality(string Nationality)
        {
            var result = await Mediator.Send(new GetAuthorByNationalityQuery(Nationality));

            return NewResult(result);
        }
    }
}