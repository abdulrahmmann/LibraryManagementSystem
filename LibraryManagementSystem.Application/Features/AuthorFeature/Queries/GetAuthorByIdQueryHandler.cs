using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.AuthorFeature.DTOs;
using LibraryManagementSystem.Application.UOF;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Features.AuthorFeature.Queries
{
    public class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, BaseResponse<AuthorDTO>>
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


        public async Task<BaseResponse<AuthorDTO>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var author = await _unitOfWork.AuthorRepository.GetById(request.Id);

                if (author is null)
                {
                    _logger.LogWarning("Authors Is Null or Empty!");

                    return BaseResponse<AuthorDTO>.NoContentResponse("Authors DTO Is Null or Empty!");
                }

                var authorDto = _mapper.Map<AuthorDTO>(author!);

                if (authorDto is null)
                {
                    _logger.LogWarning("Authors DTO Is Null or Empty!");

                    return BaseResponse<AuthorDTO>.NoContentResponse("Authors DTO Is Null or Empty!");
                }

                _logger.LogInformation("Successfully Retrieving Author By Id: {Id}", request.Id);

                return BaseResponse<AuthorDTO>.SuccessResponse(authorDto, $"Successfully Retrieving Author By Id: {request.Id}");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while Retrieving authors.");

                return BaseResponse<AuthorDTO>.InternalServerErrorResponse("An unexpected error occurred.");
            }
        }
    }
}
