using System;
using Infrastructure.Authentication.Data;
using Microsoft.AspNetCore.Builder;

namespace WebApplication.Extensions
{
    public static class AppExtensions
    {
        public static void UsePermissionInitializerExtension(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            //initialize permissions from class
            PermissionInitializer.InitializePermission(serviceProvider).Wait();
        }
    }
}
