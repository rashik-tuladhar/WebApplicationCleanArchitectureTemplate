using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Repositories
            services.AddTransient(typeof(IDapperDao), typeof(DapperDao));
            services.AddTransient<IGenericRepositoryDapper, GenericRepositoryDapper>();
            #endregion
        }
    }
}
