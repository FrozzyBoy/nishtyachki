using System.Data.Entity;
using UsersQueue.Queue.Statistics;
using UsersQueue.Queue.UserInformtion;
using UsersQueue.Services.TransferObjects;

namespace UsersQueue.Model
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserInfo> UsersInfo { get; set; }
        public DbSet<Stats> Stats { get; set; }
        public DbSet<NishtiakTransferObject> Nishtiaki { get; set; }
        public DbSet<NishtiakLogs> NishtiakLogs { get; set; }

        public AppDbContext()
            : base("DefaultConnection")
        { }

    }
}
