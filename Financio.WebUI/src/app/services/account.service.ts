import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import {
    Account,
    AccountReference,
    AccountSide,
    FormularEntry,
} from '../models/entities';
import { FormularEntryService } from './formular-entry.service';

@Injectable({
    providedIn: 'root',
})
export class AccountService {
    private accounts: Account[];

    constructor(private formularEntryService: FormularEntryService) {
        this.accounts = this.initAccounts();
    }

    public findByName(name: string | null): Observable<Account[]> {
        return from(
            name === null
                ? []
                : [this.accounts.filter((a) => a.name.includes(name))]
        );
    }

    public findByNumber(number: string): Observable<Account[]> {
        return from([this.accounts.filter((a) => a.number.includes(number))]);
    }

    public findByNameOrNumber(nameOrNumber: string): Observable<Account[]> {
        // if first letter is a digit
        if ('0123456789'.includes(nameOrNumber[0])) {
            return this.findByNumber(nameOrNumber);
        }

        return this.findByName(nameOrNumber);
    }

    public getByNumber(accountNumber: string): Observable<Account | undefined> {
        return from([this.accounts.find((a) => a.number === accountNumber)]);
    }

    public addAccount(account: Account) {
        this.accounts.push(account);
    }

    public deleteAccount(account: Account) {
        const accountIndex = this.accounts.findIndex(
            (a) => a.number === account.number
        );
        this.accounts.splice(accountIndex, 1);
    }

    public getAll(): Observable<Account[]> {
        return from([this.accounts]);
    }

    private initAccounts(): Account[] {
        const accounts: Account[] = [
            new Account('2700', 'Kassa', 'Barer Geldbestand'),
            new Account('2800', 'Bank', 'Geldbestand am Konto'),
            new Account('2500', 'Vst', 'Vorsteuer Zahlungen'),
            new Account(
                '9000',
                'Eigenkapital',
                'Gesamtkapital das zur Verfügung steht'
            ),
            new Account(
                '9890',
                'Gewinn und Verlust',
                'Abbrechnungskonto für alle Aufwände und Erträge'
            ),
            new Account('3700', 'Darlehen', 'Aufgenommene Kredite'),
        ];

        let booking = new FormularEntry(new Date(), 'Aufnahme eines Kredites');
        booking.debitAccounts.push({
            account: accounts[1],
            side: AccountSide.Debit,
            value: 5000,
        });
        booking.creditAccounts.push({
            account: accounts[5],
            side: AccountSide.Credit,
            value: 5000,
        });
        this.formularEntryService.bookFormularEntry(booking);

        booking = new FormularEntry(
            new Date(),
            'Abheben vom Bankkonto und speichern in Kasse'
        );
        booking.debitAccounts.push({
            account: accounts[0],
            side: AccountSide.Debit,
            value: 2000,
        });
        booking.creditAccounts.push({
            account: accounts[1],
            side: AccountSide.Credit,
            value: 2000,
        });
        this.formularEntryService.bookFormularEntry(booking);

        booking = new FormularEntry(
            new Date(),
            'Abheben vom Bankkonto und speichern in Kasse'
        );
        booking.debitAccounts.push({
            account: accounts[0],
            side: AccountSide.Debit,
            value: 1000,
        });
        booking.debitAccounts.push({
            account: accounts[2],
            side: AccountSide.Debit,
            value: 200,
        });
        booking.creditAccounts.push({
            account: accounts[1],
            side: AccountSide.Credit,
            value: 1200,
        });
        this.formularEntryService.bookFormularEntry(booking);

        return accounts;
    }
}
