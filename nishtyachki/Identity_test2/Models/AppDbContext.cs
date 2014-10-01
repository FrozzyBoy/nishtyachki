using System.Data.Entity;

namespace AdminApp.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("DefaultConnection")
        { }

    }
}