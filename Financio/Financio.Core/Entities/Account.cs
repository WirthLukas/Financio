using LeoBase.Backend.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Financio.Core.Entities;

public class Account : IVersionable<byte[]>, IIdentifiable<string>
{
    public static readonly Account Empty = new ();

    public string Id { get => Number; set => Number = value; }
    public string Number { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();


    public IList<AccountReference> References { get; set; } = new List<AccountReference> ();

    public IList<AccountReference> CounterAccountReferences { get; set; } = new List <AccountReference> ();

    //public IList<AccountReference> DebitSideAccountings { get; set; } = new List<AccountReference>();
    //public IList<AccountReference> CreditSideAccountings { get; set; } = new List<AccountReference>();

    internal void AddAccounting(AccountReference accountReference)
    {
        //var list = accountReference.Side == AccountSide.Debit ? DebitSideAccountings : CreditSideAccountings;
        //list.Add(accountReference);
        
        References.Add(accountReference);
    }

    public string ToTableString()
    {
        var sb = new StringBuilder();

        var title = $"{Name,-30} | {"Soll",-10} | {"Haben",-10}";
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));

        var debitSideAccountings = References.Where(r => r.Side == AccountSide.Debit).ToArray();
        var creditSideAccountings = References.Where(r => r.Side == AccountSide.Credit).ToArray();

        foreach (var ar in debitSideAccountings)
        {
            sb.AppendLine($"{ar.Date.ToShortDateString(),-10} {ar.GetAccountsString(),-20}| {ar.Value,-10} | ");
        }

        foreach (var ar in creditSideAccountings)
        {
            sb.AppendLine($"{ar.Date.ToShortDateString(),-10} {ar.GetAccountsString(),-20}| {"",-10} | {ar.Value,-10}");
        }

        sb.AppendLine(new string('-', title.Length));

        var sumOfDebit = debitSideAccountings.Sum(a => a.Value);
        var sumOfCredit = creditSideAccountings.Sum(a => a.Value);
        sb.AppendLine($"{"",30} | {sumOfDebit,-10} | {sumOfCredit,-10}");

        return sb.ToString();
    }

    public override string ToString() => $"{Name}";
}
