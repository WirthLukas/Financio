using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Financio.Core.Entities;

public class Account
{
    public static readonly Account Empty = new ();

    public string Number { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public IList<AccountReference> DebitSideAccountings { get; set; } = new List<AccountReference>();
    public IList<AccountReference> CreditSideAccountings { get; set; } = new List<AccountReference>();

    internal void AddAccounting(AccountReference ar)
    {
        var list = ar.Side == AccountSide.Debit ? DebitSideAccountings : CreditSideAccountings;
        list.Add(ar);
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
