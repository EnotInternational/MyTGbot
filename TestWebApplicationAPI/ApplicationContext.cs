using Microsoft.EntityFrameworkCore;
using TgBotClassLibrary;

namespace TestWebApplicationAPI
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserInfo> Users => Set<UserInfo>();
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=myappdab.db");
        }
    }
}
