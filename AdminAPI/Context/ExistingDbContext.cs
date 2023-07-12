using Microsoft.EntityFrameworkCore;
using ProjektAPI.Models;
using AdminAPI.Models;

namespace AdminAPI.Context
{
    public class ExistingDbContext : DbContext
    {
        
        public ExistingDbContext(DbContextOptions<ExistingDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=HomeExpensesAppDB;Trusted_Connection=True;",
                    //optionsBuilder.UseSqlServer("Data Source=tcp:homeexpensesappdbdbserver.database.windows.net,1433;Initial Catalog=HomeExpensesAppDB;User Id=DbAdmin@homeexpensesappdbdbserver;Password=P@$$w0rd",
                    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "Identity"));
        }
        public DbSet<AdminAPI.Models.Admin>? Admin { get; set; }

    }
}
