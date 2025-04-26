using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.AuthorFeature.DTOs;
using LibraryManagementSystem.Application.UOF;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Features.AuthorFeature.Queries
{
    public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, BaseResponse<IEnumerable<AuthorDto>>>
    {
        #region INSTANCE FIELDS
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllAuthorsQueryHandler> _logger;
        #endregion


        #region INJECT INSTANCES INTO CONSTRUCTOR
        public GetAllAuthorsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<GetAllAuthorsQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion


        public async Task<BaseResponse<IEnumerable<AuthorDto>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var authors = await _unitOfWork.AuthorRepository.GetAllData();

                if (authors == null || !authors.Any())
                {
                    _logger.LogWarning("Authors Is Null or Empty!");

                    return BaseResponse<IEnumerable<AuthorDto>>.NoContentResponse("Authors Is Null or Empty!");
                }

                var authorsDto = _mapper.Map<IEnumerable<AuthorDto>>(authors);

                if (authorsDto == null || !authorsDto.Any())
                {
                    _logger.LogWarning("Authors DTO Is Null or Empty!");

                    return BaseResponse<IEnumerable<AuthorDto>>.NoContentResponse("Authors DTO Is Null or Empty!");
                }

                return BaseResponse<IEnumerable<AuthorDto>>.SuccessResponse(authorsDto, "Successfully Retrieving All Authors");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while Retrieving authors.");

                return BaseResponse<IEnumerable<AuthorDto>>.InternalServerErrorResponse("An unexpected error occurred.");
            }
        }
    }
}
