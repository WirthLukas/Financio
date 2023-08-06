using Financio.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;

namespace Financio.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Account> Accounts { get; private set; } = null!;
    public DbSet<AccountReference> References { get; private set; } = null!;

    public ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        Debug.Write(configuration.ToString());
        string connectionString = configuration["ConnectionStrings:DefaultConnection"] ?? string.Empty;

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Account>()
        //     .HasMany(a => a.CounterAccountReferences)
        //     .WithMany(ar => ar.CounterAccounts)
        //     .UsingEntity(
        //         l => l.HasOne(typeof(AccountReference)).WithMany().OnDelete(DeleteBehavior.NoAction),
        //         r => r.HasOne(typeof(Account)).WithMany().OnDelete(DeleteBehavior.NoAction)
        //     );
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
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
