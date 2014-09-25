using System.Data.Entity;
using UsersQueue.Queue.UserInformtion;

namespace UsersQueue.Queue.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserInfo> UsersInfo { get; set; }
        //public DbSet<UserStats> UserStats { get; set; }

        public DbSet<Stats> Stats { get; set; }

        public AppDbContext()
            : base("DefaultConnection")
        { }

    }
}
