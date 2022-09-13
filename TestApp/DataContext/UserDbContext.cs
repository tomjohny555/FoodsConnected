using Microsoft.EntityFrameworkCore;
using TestApp.BO;

namespace TestApp.DataContext
{
    public class UserDbContext : DbContext
    {
        public DbSet<UserDetails> userDetails { get; set; }
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
    
        }   
    }
}
