using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.AuthorFeature.DTOs;
using LibraryManagementSystem.Application.UOF;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LibraryManagementSystem.Application.Features.AuthorFeature.Queries
{
    public class FilterAuthorByNameQueryHandler : IRequestHandler<FilterAuthorByNameQuery, BaseResponse<IQueryable<AuthorDto>>>
    {
        #region INSTANCE FIELDS
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<FilterAuthorByNameQueryHandler> _logger;
        #endregion

        #region CONSTRUCTOR
        public FilterAuthorByNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,
            ILogger<FilterAuthorByNameQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        #endregion

        public async Task<BaseResponse<IQueryable<AuthorDto>>> Handle(FilterAuthorByNameQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var authorsQuery = _unitOfWork.AuthorRepository.FilterAuthorByName(request.searchName);

                var authorList = authorsQuery.ToList();

                if (!authorList.Any())
                {
                    _logger.LogWarning("No authors found matching the name: {SearchName}", request.searchName);

                    return BaseResponse<IQueryable<AuthorDto>>.NoContentResponse("No authors found.");
                }

                var authorsDto = _mapper.Map<List<AuthorDto>>(authorList);

                if (!authorsDto.Any())
                {
                    _logger.LogWarning("Mapping to AuthorDTO resulted in an empty list for name: {SearchName}", request.searchName);

                    return BaseResponse<IQueryable<AuthorDto>>.NoContentResponse("No authors found after mapping.");
                }

                return BaseResponse<IQueryable<AuthorDto>>.SuccessResponse(authorsDto.AsQueryable(), "Successfully retrieved authors.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving authors for name: {SearchName}", request.searchName);

                return BaseResponse<IQueryable<AuthorDto>>.InternalServerErrorResponse("An unexpected error occurred.");
            }
        }
    }
}
