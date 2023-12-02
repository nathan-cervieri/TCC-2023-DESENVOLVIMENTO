using AngularVersionConverter.Application.Interfaces;
using AngularVersionConverter.Application.Services;
using AngularVersionConverter.Infra.Interfaces;
using AngularVersionConverter.Infra.Repositories.Memory;

namespace AngularVersionConverter.Api.Extensions
{
    public static class ServiceInjectionExtension
    {
        public static void AddConverterServices(this IServiceCollection services)
        {
            services.AddScoped<IConverterService, ConverterService>();
            services.AddScoped<IVersionChangeRepository, MemoryVersionChangeRepository>();
        }
    }
}
