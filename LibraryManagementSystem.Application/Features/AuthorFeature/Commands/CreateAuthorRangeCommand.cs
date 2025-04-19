﻿using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.AuthorFeature.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.AuthorFeature.Commands
{
    public record CreateAuthorRangeCommand(IEnumerable<AuthorDTO> AuthorDTOs) : IRequest<BaseResponse<bool>>;
}