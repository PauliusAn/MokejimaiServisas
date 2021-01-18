using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=127.0.0.1,1234;Initial Catalog=Mokejimai;Persist Security Info=True;User ID=sa;Password=<YourStrong!Passw0rd>");
        }
    }
}