using LibraryManagementSystem.Application.Features.BookFeature.Commands.Requests;
using LibraryManagementSystem.Application.Features.BookFeature.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : AppControllerBase
    {
        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBook(CreateBookDto createBookDto)
        {
            var result = await Mediator.Send(new CreateBookCommand(createBookDto));

            return NewResult(result);
        }
    }
}
