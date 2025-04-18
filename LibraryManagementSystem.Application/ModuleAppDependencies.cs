using Microsoft.Extensions.DependencyInjection;

namespace LibraryManagementSystem.Application
{
    public static class ModuleAppDependencies
    {
        public static IServiceCollection AddAppDependencies(this IServiceCollection service)
        {
            return service;
        }
    }
}
