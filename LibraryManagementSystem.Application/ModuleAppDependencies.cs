﻿using FluentValidation;
using LibraryManagementSystem.Application.Features.AuthorFeature.Validators;
using LibraryManagementSystem.Application.UOF;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LibraryManagementSystem.Application
{
    public static class ModuleAppDependencies
    {
        public static IServiceCollection AddAppDependencies(this IServiceCollection service)
        {
            // Register MediatR
            service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


            // Register Mapster
            service.AddMapster();


            // Register FluentValidation
            service.AddValidatorsFromAssemblyContaining<AuthorValidator>();


            // REGISTER UNIT OF WORK
            service.AddScoped<IUnitOfWork, UnitOfWork>();

            return service;
        }
    }
}
