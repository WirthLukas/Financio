// See https://aka.ms/new-console-template for more information
using Financio.Core.Entities;
using Financio.Persistence;

//var kassa = new Account() { Name = "Kassa" };
//var bank = new Account() { Name = "Bank" };
//var ek = new Account() { Name = "EK" };
//var vst = new Account() { Name = "Vst" };

//var date = DateTime.Now;

//new FormularEntry.Builder()
//    .SetDate(date)
//    .AddAccount(kassa, 100, AccountSide.Debit)
//    .AddAccount(vst, 20, AccountSide.Debit)
//    .AddAccount(ek, 120, AccountSide.Credit)
//    .Build();

//new FormularEntry.Builder().SetDate(date)
//    .AddAccount(debitAccount: kassa, creditAccount: ek, value: 1100)
//    .Build();

//new FormularEntry.Builder().SetDate(date)
//    .AddAccount(debitAccount: kassa, creditAccount: ek, value: 2000)
//    .Build();

//new FormularEntry.Builder().SetDate(date)
//    .AddAccount(debitAccount: bank, creditAccount: ek, value: 10000)
//    .Build();

//new FormularEntry.Builder().SetDate(date)
//    .AddAccount(debitAccount: bank, creditAccount: kassa, value: 2000)
//    .Build();

//new FormularEntry.Builder().SetDate(date)
//    .AddAccount(debitAccount: kassa, creditAccount: bank, value: 1000)
//    .Build();

//Console.WriteLine(kassa.ToTableString());
//Console.WriteLine();
//Console.WriteLine(bank.ToTableString());
//Console.WriteLine();
//Console.WriteLine(ek.ToTableString());

var accounts = new Dictionary<string, Account>
        {
            { "Kassa", new Account { Number = "2700", Name = "Kassa", Description = "Barer Geldbestand" }  },
            { "Bank", new Account { Number = "2800", Name = "Bank", Description = "Geldbestand am Konto" } },
            { "Vst", new Account { Number = "2500", Name = "Vst", Description = "Vorsteuer Zahlungen" } },
            { "Eigenkapital", new Account { Number = "9000", Name = "Eigenkapital", Description = "Gesamtkapital das zur Verfügung steht" } },
            { "Gewinn und Verlust", new Account { Number = "9890", Name = "Gewinn und Verlust", Description = "Abbrechnungskonto für alle Aufwände und Erträge" } },
            { "Darlehen", new Account { Number = "3700", Name = "Darlehen", Description = "Aufgenommene Kredite" } }
        };

new FormularEntry.Builder()
    .SetDate(DateTime.Now)
    .SetTitle("Aufnahme eines Kredites")
    .AddAccount(accounts["Bank"], accounts["Darlehen"], 5000)
    .Build();

new FormularEntry.Builder()
    .SetDate(DateTime.Now)
    .SetTitle("Abheben vom Bankkonto und speichern in Kasse")
    .AddAccount(accounts["Kassa"], accounts["Bank"], 2000)
    .Build();

new FormularEntry.Builder()
    .SetDate(DateTime.Now)
    .SetTitle("Abheben vom Bankkonto und speichern in Kasse")
    .AddAccount(accounts["Kassa"], 1000, AccountSide.Debit)
    .AddAccount(accounts["Vst"], 200, AccountSide.Debit)
    .AddAccount(accounts["Bank"], 1200, AccountSide.Credit)
    .Build();

using var unitOfWork = new UnitOfWork();

AccountReference[] references = accounts.Values
    .SelectMany(a => a.References)
    .Distinct()
    .ToArray();

await unitOfWork.AccountReferences.AddRangeAsync(references);
await unitOfWork.SaveChangesAsync();
