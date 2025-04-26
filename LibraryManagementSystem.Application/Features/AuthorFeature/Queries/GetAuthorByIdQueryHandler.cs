using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.AuthorFeature.DTOs;
using LibraryManagementSystem.Application.UOF;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Features.AuthorFeature.Queries
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, BaseResponse<AuthorDto>>
    {
        #region INSTANCE FIELDS
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAuthorByIdQueryHandler> _logger;
        #endregion


        #region INJECT INSTANCES INTO CONSTRUCTOR
        public GetAuthorByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<GetAuthorByIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion


        public async Task<BaseResponse<AuthorDto>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var author = await _unitOfWork.AuthorRepository.GetById(request.Id);

                if (author is null)
                {
                    _logger.LogWarning("Authors Is Null or Empty!");

                    return BaseResponse<AuthorDto>.NoContentResponse("Authors DTO Is Null or Empty!");
                }

                var authorDto = _mapper.Map<AuthorDto>(author!);

                if (authorDto is null)
                {
                    _logger.LogWarning("Authors DTO Is Null or Empty!");

                    return BaseResponse<AuthorDto>.NoContentResponse("Authors DTO Is Null or Empty!");
                }

                _logger.LogInformation("Successfully Retrieving Author By Id: {Id}", request.Id);

                return BaseResponse<AuthorDto>.SuccessResponse(authorDto, $"Successfully Retrieving Author By Id: {request.Id}");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while Retrieving authors.");

                return BaseResponse<AuthorDto>.InternalServerErrorResponse("An unexpected error occurred.");
            }
        }
    }
}
