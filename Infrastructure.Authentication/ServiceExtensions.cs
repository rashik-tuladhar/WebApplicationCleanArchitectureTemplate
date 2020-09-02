using System;
using Application.Interfaces.CoreSetup.RoleManagement;
using Application.Interfaces.CoreSetup.UserManagement;
using Infrastructure.Authentication.Contexts;
using Infrastructure.Authentication.Models.Identity;
using Infrastructure.Authentication.Services.RoleManagement;
using Infrastructure.Authentication.Services.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Authentication
{
    public static class ServiceExtensions
    {
        public static void AddAuthenticationInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthenticationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequiredLength = 1;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationDbContext>()
                .AddDefaultTokenProviders();

            //additional claims besides the default lists
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AdditionalClaims>();

            services.AddTransient<IRoleExtension, RoleExtension>();
            services.AddTransient<IRoleManagementBusiness, RoleManagementBusiness>();
            services.AddTransient<IRoleManagementRepository, RoleManagementRepository>();
            services.AddTransient<IUserManagementBusiness, UserManagementBusiness>();
            services.AddTransient<IUserManagementRepository, UserManagementRepository>();

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToDouble(configuration["ApplicationData:ApplicationTimeOut"]));
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Login";
                options.LogoutPath = "/Login/Logout";
                options.AccessDeniedPath = "/Login/AccessDenied";
                options.SlidingExpiration = true;
            });
        }
    }
}
