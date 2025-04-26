using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.AuthorFeature.DTOs;
using LibraryManagementSystem.Application.UOF;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Features.AuthorFeature.Queries
{
    public class GetAuthorByNameQueryHandler : IRequestHandler<GetAuthorByNameQuery, BaseResponse<AuthorDto>>
    {
        #region INSTANCE FIELDS
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAuthorByNameQueryHandler> _logger;
        #endregion


        #region INJECT INSTANCES INTO CONSTRUCTOR
        public GetAuthorByNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<GetAuthorByNameQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion

        public async Task<BaseResponse<AuthorDto>> Handle(GetAuthorByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var author = await _unitOfWork.AuthorRepository.GetAuthorByNameAsync(request.Name);

                if (author is null)
                {
                    _logger.LogWarning("Author Is Null or Empty!");

                    return BaseResponse<AuthorDto>.NoContentResponse("Authors DTO Is Null or Empty!");
                }

                var authorDto = _mapper.Map<AuthorDto>(author!);

                if (authorDto is null)
                {
                    _logger.LogWarning("Author DTO Is Null or Empty!");

                    return BaseResponse<AuthorDto>.NoContentResponse("Authors DTO Is Null or Empty!");
                }

                _logger.LogInformation("Successfully Retrieving Author By Name: {Name}", request.Name);

                return BaseResponse<AuthorDto>.SuccessResponse(authorDto, $"Successfully Retrieving Author By Name: {request.Name}");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while Retrieving authors.");

                return BaseResponse<AuthorDto>.InternalServerErrorResponse("An unexpected error occurred.");
            }
        }
    }
}
