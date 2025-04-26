using LibraryManagementSystem.Application.Features.GenreFeature.Commands.Requests;
using LibraryManagementSystem.Application.Features.GenreFeature.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : AppControllerBase
    {
        [HttpPost("CreateGenre")]
        public async Task<IActionResult> CreateGenreRange([FromBody] GenreDto genreDto)
        {
            var result = await Mediator.Send(new CreateGenreCommand(genreDto));

            return NewResult(result);
        }
        
        [HttpPost("CreateGenreRange")]
        public async Task<IActionResult> CreateGenre([FromBody] IEnumerable<GenreDto> genreDtos)
        {
            var result = await Mediator.Send(new CreateGenreRangeCommand(genreDtos));

            return NewResult(result);
        }
    }
}
