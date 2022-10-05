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

    [NotMapped] public string Id { get => Number; set => Number = value; }
    [Key, MaxLength(4)] public string Number { get; set; } = string.Empty;
    [MaxLength(200)] public string Name { get; set; } = string.Empty;
    [MaxLength(500)] public string? Description { get; set; }

    [Timestamp] public byte[] RowVersion { get; set; } = Array.Empty<byte>();

    [NotMapped] public IList<AccountReference> DebitSideAccountings { get; set; } = new List<AccountReference>();
    [NotMapped] public IList<AccountReference> CreditSideAccountings { get; set; } = new List<AccountReference>();

    internal void AddAccounting(AccountReference accountReference)
    {
        var list = accountReference.Side == AccountSide.Debit ? DebitSideAccountings : CreditSideAccountings;
        list.Add(accountReference);
    }

    public string ToTableString()
    {
        var sb = new StringBuilder();

        var title = $"{Name,-30} | {"Soll",-10} | {"Haben",-10}";
        sb.AppendLine(title);
        sb.AppendLine(new string('-', title.Length));

        foreach (var ar in DebitSideAccountings)
        {
            sb.AppendLine($"{ar.Date.ToShortDateString(),-10} {ar.GetAccountsString(),-20}| {ar.Value,-10} | ");
        }

        foreach (var ar in CreditSideAccountings)
        {
            sb.AppendLine($"{ar.Date.ToShortDateString(),-10} {ar.GetAccountsString(),-20}| {"",-10} | {ar.Value,-10}");
        }

        sb.AppendLine(new string('-', title.Length));

        var sumOfDebit = DebitSideAccountings.Sum(a => a.Value);
        var sumOfCredit = CreditSideAccountings.Sum(a => a.Value);
        sb.AppendLine($"{"",30} | {sumOfDebit,-10} | {sumOfCredit,-10}");

        return sb.ToString();
    }

    public override string ToString() => $"{Name}";
}
