﻿using LibraryManagementSystem.Application.Common;
using LibraryManagementSystem.Application.Features.GenreFeature.DTOs;
using MediatR;

namespace LibraryManagementSystem.Application.Features.GenreFeature.Commands.Requests;

public record CreateGenreCommand(GenreDto GenreDto): IRequest<BaseResponse<bool>>;