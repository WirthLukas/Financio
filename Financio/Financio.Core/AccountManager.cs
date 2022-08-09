using Financio.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financio.Core;

internal class AccountManager
{
    public readonly Account EBK = new() 
    {
        Number = "9800",
        Name = "Eröffnungsbilanzkonto",
    };

    public readonly Account SKB = new()
    {
        Number = "9850",
        Name = "Schlussbilanzkonto",
    };

    public void CreateAccount(Account account)
    {
        var formularEntry = new FormularEntry.Builder()
            .SetDate(DateTime.Now)
            .SetTitle($"Creation of Account {account.Name}")
            .AddAccount(debitAccount: EBK, creditAccount: account, value: 0)
            .Build();
    }
}
