using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authentication.Models.Identity
{
    public class AdditionalClaims : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public AdditionalClaims(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("FullName", string.Concat(user.FirstName, string.IsNullOrEmpty(user.MiddleName)?" ":" "+user.MiddleName, " " + user.LastName) ?? ""));
            return identity;
        }
    }
}
