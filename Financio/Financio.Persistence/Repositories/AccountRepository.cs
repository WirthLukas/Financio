using Financio.Core.Contracts;
using Financio.Core.Entities;
using LeoBase.Backend.Common;

namespace Financio.Persistence.Repositories;

internal class AccountRepository : GenericRepository<Account, string, byte[]>, IAccountRepository
{
    public AccountRepository(ApplicationDbContext dbContext) : base(dbContext, dbContext.Accounts)
    {
    }
}
