using System;
using System.Threading.Tasks;

namespace Financio.Core.Contracts;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    IAccountRepository Accounts { get; }

    Task<int> SaveChangesAsync();
    Task DeleteDatabaseAsync();
    Task MigrateDatabaseAsync();
    Task CreateDatabaseAsync();

    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}
