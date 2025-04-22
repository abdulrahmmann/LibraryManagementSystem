using LibraryManagementSystem.Application.Features.PublisherFeature.Commands.Requests;
using LibraryManagementSystem.Application.Features.PublisherFeature.DTOs;
using LibraryManagementSystem.Application.Features.PublisherFeature.Queries.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : AppControllerBase
    {
        [HttpGet("GetAllPublishers")]
        public async Task<IActionResult> GetAllPublishers()
        {
            var result = await Mediator.Send(new GetAllPublisherQuery());

            return NewResult(result);
        }
        
        [HttpGet("GetPublisherById")]
        public async Task<IActionResult> GetPublisherById(int Id)
        {
            var result = await Mediator.Send(new GetPublisherByIdQuery(Id));

            return NewResult(result);
        }
        
        [HttpGet("GetPublisherByName")]
        public async Task<IActionResult> GetPublisherByName(string Name)
        {
            var result = await Mediator.Send(new GetPublisherByNameQuery(Name));

            return NewResult(result);
        }
        
        [HttpGet("GetPublisherByEmail")]
        public async Task<IActionResult> GetPublisherByEmail(string Email)
        {
            var result = await Mediator.Send(new GetPublisherByEmailQuery(Email));

            return NewResult(result);
        }
        
        [HttpGet("GetPublisherByPhoneNumber")]
        public async Task<IActionResult> GetPublisherByPhoneNumber(string PhoneNumber)
        {
            var result = await Mediator.Send(new GetPublisherByPhoneQuery(PhoneNumber));

            return NewResult(result);
        }
        
        [HttpGet("FilterPublisherByName")]
        public async Task<IActionResult> FilterPublisherByName(string searchName)
        {
            var result = await Mediator.Send(new FilterPublisherByNameQuery(searchName));

            return NewResult(result);
        }
        
        [HttpGet("FilterPublisherByEmail")]
        public async Task<IActionResult> FilterPublisherByEmail(string searchEmail)
        {
            var result = await Mediator.Send(new FilterPublisherByEmailQuery(searchEmail));

            return NewResult(result);
        }
        
        [HttpPost("CreatePublisher")]
        public async Task<IActionResult> CreatePublisher([FromBody] PublisherDTO publisherDto)
        {
            var result = await Mediator.Send(new CreatePublisherCommand(publisherDto));
            
            return NewResult(result);
        }

        [HttpPost("CreateRangePublisher")]
        public async Task<IActionResult> CreateRangePublisher([FromBody] IEnumerable<PublisherDTO> publisherDtos)
        {
            var result = await Mediator.Send(new CreatePublishersRangeCommand(publisherDtos));
            
            return NewResult(result);
        }
    }
}
