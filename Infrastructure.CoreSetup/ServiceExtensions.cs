using Application.Interfaces.CoreSetup.RoleManagement;
using Infrastructure.CoreSetup.Services.RoleManagement;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.CoreSetup
{
    public static class ServiceExtensions
    {
        public static void AddCoreSetupInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<IRoleExtension, RoleExtension>();
            services.AddTransient<IRoleManagementBusiness, RoleManagementBusiness>();
            services.AddTransient<IRoleManagementRepository, RoleManagementRepository>();
        }
    }
}
