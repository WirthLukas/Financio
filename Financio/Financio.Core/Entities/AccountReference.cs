using LeoBase.Backend.Common;
using System;
using System.Linq;

namespace Financio.Core.Entities;

public class AccountReference : EntityObject
{
    public DateTime Date { get; set; }
    public Account Account { get; set; } = Account.Empty; 
    public Account[] CounterAccounts { get; set; } = Array.Empty<Account>();
    public double Value { get; set; }
    public AccountSide Side { get; set; }

    public string GetAccountsString() => CounterAccounts
            .Select(acc => $"{acc.Number} {acc.Name}")
            .Aggregate((result, next) => $"{result}, {next}");

    public override string ToString()
    {
        return $"{Account}: {Date.ToShortDateString()} {GetAccountsString()} {Value}";
    }
}

public enum AccountSide: byte
{ 
    /// <summary>
    /// Soll Seite
    /// </summary>
    Debit,
    /// <summary>
    /// Haben Seite
    /// </summary>
    Credit
}

public static class AccoundSideExtension
{
    public static AccountSide Other(this AccountSide side)
    {
        //return side == AccountSide.Debit ? AccountSide.Credit : AccountSide.Debit;
        return (AccountSide)(((byte)side + 1) % Enum.GetValues<AccountSide>().Length);
    }
}
