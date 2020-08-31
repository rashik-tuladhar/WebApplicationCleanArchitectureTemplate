using Infrastructure.Authentication.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Authentication.Contexts
{
    public class AuthenticationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string,
        IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>, IdentityRoleClaim<string>,
        IdentityUserToken<string>>
    {
        public DbSet<RolePermission> RolePermission { get; set; }
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
            : base(options)
        {
        }


        /// <summary>
        /// Renaming The Tables of Default Identity Values
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(x => { x.ToTable("AuthUsers", "Authentication"); });
            builder.Entity<IdentityUserRole<string>>(x => { x.ToTable("AuthUserRoles", "Authentication"); });
            builder.Entity<IdentityUserLogin<string>>(x => { x.ToTable("AuthUserLogins", "Authentication"); });
            builder.Entity<IdentityUserClaim<string>>(x => { x.ToTable("AuthUserClaims", "Authentication"); });
            builder.Entity<IdentityRole>(x => { x.ToTable("AuthRoles", "Authentication"); });
            builder.Entity<IdentityRoleClaim<string>>(x => { x.ToTable("AuthRoleClaims", "Authentication"); });
            builder.Entity<IdentityUserToken<string>>(x => { x.ToTable("AuthUserTokens", "Authentication"); });
            builder.Entity<RolePermission>(x => { x.ToTable("AuthRolePermissions", "Authentication"); });
        }
    }
}
