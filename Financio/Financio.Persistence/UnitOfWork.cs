using Financio.Core.Contracts;
using Financio.Persistence.Repositories;
using LeoBase.Backend.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Financio.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private bool _disposed;
    private IDbContextTransaction? _transaction = null;

    public IAccountRepository Accounts { get; }
    public IAccountReferenceRepository AccountReferences { get; }

    public UnitOfWork(DbContextOptions<ApplicationDbContext>? options = null)
    {
        _dbContext = new ApplicationDbContext(options ?? new DbContextOptions<ApplicationDbContext>());
        Accounts = new AccountRepository(_dbContext);
        AccountReferences = new AccountReferenceRepository(_dbContext);
    }

    public async Task BeginTransactionAsync()
    {
        if (_transaction is not null)
        {
            await _transaction.DisposeAsync();
        }

        _transaction = await _dbContext.Database.BeginTransactionAsync();
    }

    public Task CommitAsync() => _transaction?.CommitAsync() ?? Task.CompletedTask;
    public Task RollbackAsync() => _transaction?.RollbackAsync() ?? Task.CompletedTask;

    public Task CreateDatabaseAsync() => _dbContext.Database.EnsureCreatedAsync();
    public Task DeleteDatabaseAsync() => _dbContext.Database.EnsureDeletedAsync();
    public Task MigrateDatabaseAsync() => _dbContext.Database.MigrateAsync();

    public Task<int> SaveChangesAsync()
    {
        var entities = _dbContext.ChangeTracker.Entries()
            .Where(entity => entity.State == EntityState.Added || entity.State == EntityState.Modified)
            .Select(e => e.Entity)
            .ToArray();  // Geänderte Entities ermitteln

        EntityValidator.Validate(this, entities);

        return _dbContext.SaveChangesAsync();
    }

    #region <<IDisposable>>

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _dbContext.Dispose();
            _transaction?.Dispose();
        }

        _disposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        // implementation like https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-disposeasync

        await DisposeAsync(true);
        Dispose(disposing: false);
        GC.SuppressFinalize(this);
    }

    protected virtual async ValueTask DisposeAsync(bool disposing)
    {
        if (!_disposed && disposing)
        {
            await _dbContext.DisposeAsync().ConfigureAwait(false);

            if (_transaction is not null)
                await _transaction.DisposeAsync();
        }

        _disposed = true;
    }

    #endregion
}
