using AngularVersionConverter.Application.Interfaces;
using AngularVersionConverter.Application.Services;
using AngularVersionConverter.Infra.Repositories.Mocked;
using AngularVersionConverterApplication.Interfaces.Repository;

namespace AngularVersionConverter.Api.Extensions
{
    public static class ServiceInjectionExtension
    {
        public static void AddService(this IServiceCollection services)
        {
            services.AddScoped<IConverterService, ConverterService>();
            services.AddScoped<IVersionChangeRepository, VersionChangeRepository>();
        }
    }
}
