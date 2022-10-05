using Financio.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;

namespace Financio.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Account> Accounts { get; private set; } = null!;

    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        Debug.Write(configuration.ToString());
        string connectionString = configuration["ConnectionStrings:DefaultConnection"];

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>().HasData(
            new Account { Number = "0140", Name = "Bank" },
            new Account { Number = "0740", Name = "Eigenkapital" }
            );
    }
}

// If you don't want the Database setup in the OnConfiguring method
//public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
//{
//    public ApplicationDbContext CreateDbContext(string[] args)
//    {
//        var configuration = new ConfigurationBuilder()
//            .SetBasePath(Environment.CurrentDirectory)
//            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//            .Build();

//        Debug.Write(configuration.ToString());
//        string connectionString = configuration["ConnectionStrings:DefaultConnection"];

//        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//            .UseSqlServer(connectionString)
//            .Options;

//        return new ApplicationDbContext(options);
//    }
//}
