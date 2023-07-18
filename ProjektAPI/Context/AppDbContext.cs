using Microsoft.EntityFrameworkCore;
using ProjektAPI.Configuration;
using ProjektAPI.Models;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Budget> Budgets { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relacja 1 do 1 między User a Budget
        modelBuilder.Entity<User>()
            .HasOne(u => u.Budget)
            .WithOne(b => b.User)
            .HasForeignKey<Budget>(b => b.UserId);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=HomeExpensesAppDB;Trusted_Connection=True;",
        //optionsBuilder.UseSqlServer("Data Source=tcp:homeexpensesappdbdbserver.database.windows.net,1433;Initial Catalog=HomeExpensesAppDB;User Id=DbAdmin@homeexpensesappdbdbserver;Password=P@$$w0rd",
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "Identity"));
    }
}