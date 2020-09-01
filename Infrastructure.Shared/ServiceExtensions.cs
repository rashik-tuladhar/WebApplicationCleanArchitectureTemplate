using Application.Interfaces.Shared;
using Infrastructure.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Shared
{
    public static class ServiceExtensions
    {
        public static void AddSharedInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IEncryptionService, EncryptionService>();
        }
    }
}
