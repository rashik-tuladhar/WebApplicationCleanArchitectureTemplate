using System.Linq;
using Application.Interfaces.CoreSetup.RoleManagement;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Authentication.Services.RoleManagement
{
    public class RoleExtension : IRoleExtension
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoleExtension(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public bool HasPermission(string permissionValue)
        {
            var hasPermission = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x =>
                x.Type == "permission" && x.Value == permissionValue);
            return hasPermission != null;
        }
    }
}
