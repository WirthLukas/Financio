using Financio.Core.Entities;
using LeoBase.Backend.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financio.Core.Contracts;

public interface IAccountRepository : IGenericRepository<Account, string, byte[]>
{
}
