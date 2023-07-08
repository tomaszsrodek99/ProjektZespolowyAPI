using Microsoft.EntityFrameworkCore;
using ProjektAPI.Models;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Role> Roles { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=HomeExpensesAppDB;Trusted_Connection=True;",
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "Identity"));
    }
}