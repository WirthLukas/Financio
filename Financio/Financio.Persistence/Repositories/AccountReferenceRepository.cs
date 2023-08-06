using Financio.Core.Contracts;
using Financio.Core.Entities;
using LeoBase.Backend.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financio.Persistence.Repositories;

internal class AccountReferenceRepository : GenericRepository<AccountReference, int, byte[]>, IAccountReferenceRepository
{
    public AccountReferenceRepository(DbContext dbContext, DbSet<AccountReference>? dbSet = null) : base(dbContext, dbSet)
    {
    }
}
