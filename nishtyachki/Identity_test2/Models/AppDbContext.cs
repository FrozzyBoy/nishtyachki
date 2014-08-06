using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AdminApp.Models
{
    public class AppDbContext : DbContext
    {
        private static AppDbContext _instance;
        private static object _lockInstance = new object();

        public static AppDbContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockInstance)
                    {
                        if (_instance == null)
                        {
                            _instance = new AppDbContext();
                        }
                    }
                }
                return _instance;
            }
        }

        public DbSet<UserInfo> UsersInfo { get; set; }

        private AppDbContext()
            : base("DefaultConnection") 
        { }
    }
}