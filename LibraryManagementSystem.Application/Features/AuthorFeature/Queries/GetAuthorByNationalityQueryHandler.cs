using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.AuthorFeature.DTOs;
using LibraryManagementSystem.Application.UOF;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Features.AuthorFeature.Queries
{
    public class GetAuthorByNationalityQueryHandler : IRequestHandler<GetAuthorByNationalityQuery, BaseResponse<IEnumerable<AuthorDto>>>
    {
        #region INSTANCE FIELDS
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAuthorByNationalityQueryHandler> _logger;
        #endregion


        #region INJECT INSTANCES INTO CONSTRUCTOR
        public GetAuthorByNationalityQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<GetAuthorByNationalityQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion


        public async Task<BaseResponse<IEnumerable<AuthorDto>>> Handle(GetAuthorByNationalityQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var author = await _unitOfWork.AuthorRepository.GetAuthorsByNationalityAsync(request.Nationality);

                if (author is null)
                {
                    _logger.LogWarning("Author Is Null or Empty!");

                    return BaseResponse<IEnumerable<AuthorDto>>.NoContentResponse("Author DTO Is Null or Empty!");
                }

                var authorDto = _mapper.Map<IEnumerable<AuthorDto>>(author!);

                if (authorDto is null)
                {
                    _logger.LogWarning("Author DTO Is Null or Empty!");

                    return BaseResponse<IEnumerable<AuthorDto>>.NoContentResponse("Author DTO Is Null or Empty!");
                }

                _logger.LogInformation("Successfully Retrieving Author By Nationality: {Nationality}", request.Nationality);

                return BaseResponse<IEnumerable<AuthorDto>>.SuccessResponse(authorDto, $"Successfully Retrieving Author By Nationality: {request.Nationality}");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while Retrieving authors.");

                return BaseResponse<IEnumerable<AuthorDto>>.InternalServerErrorResponse("An unexpected error occurred.");
            }
        }
    }
}
