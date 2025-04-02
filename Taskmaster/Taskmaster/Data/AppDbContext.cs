//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Taskmaster.Data
//{
//    internal class AppDbContext
//    {
//    }
//}

using Microsoft.EntityFrameworkCore;

namespace TaskManagerApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=taskmanagerdb;User=root;Password=;",
                    new MySqlServerVersion(new Version(8, 0, 30)));
            }
        }
    }
}

