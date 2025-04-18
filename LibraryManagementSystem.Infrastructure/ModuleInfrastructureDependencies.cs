using LibraryManagementSystem.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementSystem.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection service)
        {
            service.AddSingleton<DapperContext>();

            return service;
        }
    }
}
