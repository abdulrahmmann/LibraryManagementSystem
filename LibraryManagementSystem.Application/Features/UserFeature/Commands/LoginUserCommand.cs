﻿using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.UserFeature.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.UserFeature.Commands
{
    public record LoginUserCommand(LoginUserDto UserDTO) : IRequest<AuthResponse>;
}
