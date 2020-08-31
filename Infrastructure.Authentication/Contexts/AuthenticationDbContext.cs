using Infrastructure.Authentication.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Authentication.Contexts
{
    public class AuthenticationDbContext : DbContext
    {
        public DbSet<TokenAuthenticationDetails> TokenAuthenticationDetails { get; set; }

        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
        {

        }
    }
}
