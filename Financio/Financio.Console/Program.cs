// See https://aka.ms/new-console-template for more information
using Financio.Core.Entities;

var kassa = new Account() { Name = "Kassa" };
var bank = new Account() { Name = "Bank" };
var ek = new Account() { Name = "EK" };
var vst = new Account() { Name = "Vst" };

var date = DateTime.Now;

new FormularEntry.Builder()
    .SetDate(date)
    .AddAccount(kassa, 100, AccountSide.Debit)
    .AddAccount(vst, 20, AccountSide.Debit)
    .AddAccount(ek, 120, AccountSide.Credit)
    .Build();

new FormularEntry.Builder().SetDate(date)
    .AddAccount(debitAccount: kassa, creditAccount: ek, value: 1100)
    .Build();

new FormularEntry.Builder().SetDate(date)
    .AddAccount(debitAccount: kassa, creditAccount: ek, value: 2000)
    .Build();

new FormularEntry.Builder().SetDate(date)
    .AddAccount(debitAccount: bank, creditAccount: ek, value: 10000)
    .Build();

new FormularEntry.Builder().SetDate(date)
    .AddAccount(debitAccount: bank, creditAccount: kassa, value: 2000)
    .Build();

new FormularEntry.Builder().SetDate(date)
    .AddAccount(debitAccount: kassa, creditAccount: bank, value: 1000)
    .Build();

Console.WriteLine(kassa.ToTableString());
Console.WriteLine();
Console.WriteLine(bank.ToTableString());
Console.WriteLine();
Console.WriteLine(ek.ToTableString());