using System.Data.Entity;

namespace AdminApp.Models
{
    public class AppDbContext : DbContext
    {       
        public DbSet<UserInfo> UsersInfo { get; set; }
        public DbSet<UserStats> UserStats { get; set; }

        public AppDbContext()
            : base("DefaultConnection") 
        { }

    }
}