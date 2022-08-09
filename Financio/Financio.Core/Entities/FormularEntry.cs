using System;
using System.Collections.Generic;
using System.Linq;

namespace Financio.Core.Entities;

public class FormularEntry
{
    public DateTime Date { get; init; }
    public string Title { get; set; } = string.Empty;
    public List<AccountReference> DebitAccounts { get; init; } = new();
    public List<AccountReference> CreditAccounts { get; init; } = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="formularEntry"></param>
    /// <exception cref="Exception"></exception>
    public static void Validate(FormularEntry formularEntry)
    {
        var exceptions = new List<Exception>();

        if (formularEntry.DebitAccounts.Count is 0)
        {
            exceptions.Add(new Exception("Es muss mindestens ein Konto auf Soll gebucht werden"));
        }

        if (formularEntry.CreditAccounts.Count is 0)
        {
            exceptions.Add(new Exception("Es muss mindestens ein Konto auf Haben gebucht werden"));
        }

        var debitSum = formularEntry.DebitAccounts.Sum(item => item.Value);
        var creditSum = formularEntry.CreditAccounts.Sum(item => item.Value);

        if (debitSum != creditSum)
        {
            exceptions.Add(new Exception($"Soll (aktuell {debitSum}) und Haben (aktuell {creditSum}) müssen gleich sein"));
        }

        if (exceptions.Count is 1)
        {
            throw exceptions[0];
        }

        if (exceptions.Count > 0)
        {
            throw new Exception($"{formularEntry} ist kein korrekter Buchungssatz", new AggregateException(exceptions));
        }
    }

    public class Builder
    {
        public DateTime Date { get; private set; } = DateTime.Now;
        public string Title { get; set; } = string.Empty;
        public List<(Account Account, double Value)> DebitAccountings { get; } = new();
        public List<(Account Account, double Value)> CreditAccountings { get; } = new();

        public FormularEntry.Builder SetDate(DateTime date)
        {
            Date = date;
            return this;
        }

        public FormularEntry.Builder SetTitle(string title)
        {
            Title = title;
            return this;
        }

        public FormularEntry.Builder AddAccount(Account account, double value, AccountSide side)
        {
            var list = side == AccountSide.Debit ? DebitAccountings : CreditAccountings;
            list.Add((account, value));
            return this;
        }

        public FormularEntry.Builder AddAccount(Account debitAccount, Account creditAccount, double value)
        {
            DebitAccountings.Add((debitAccount, value));
            CreditAccountings.Add((creditAccount, value));
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public FormularEntry Build()
        {
            var formularEntry = new FormularEntry
            {
                Date = Date,
                Title = Title,
            };

            Book(formularEntry.DebitAccounts, formularEntry.CreditAccounts);
            Validate(formularEntry);
            return formularEntry;
        }

        private void Book(List<AccountReference> debitAccounts, List<AccountReference> creditAccounts)
        {
            var debitSideAccounts = ExtractAccounts(DebitAccountings);
            var creditSideAccounts = ExtractAccounts(CreditAccountings);

            foreach (var (account, value) in DebitAccountings)
            {
                debitAccounts.Add(new AccountReference
                {
                    Date = Date,
                    Value = value,
                    Side = AccountSide.Debit,
                    Account = account,
                    CounterAccounts = creditSideAccounts
                }.Also(AddAccountReferenceToAccount));
            }

            foreach (var (account, value) in CreditAccountings)
            {
                creditAccounts.Add(new AccountReference
                {
                    Date = Date,
                    Value = value,
                    Side = AccountSide.Credit,
                    Account = account,
                    CounterAccounts = debitSideAccounts
                }.Also(AddAccountReferenceToAccount));
            }

            static Account[] ExtractAccounts(List<(Account Account, double Value)> accountingList)
            {
                return accountingList.Select(item => item.Account).ToArray();
            }

            static void AddAccountReferenceToAccount(AccountReference ar) => ar.Account.AddAccounting(ar);
        }
    }
}
