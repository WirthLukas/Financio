import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import { FormularEntryBooking } from '../models/entities';
import { AccountReference, FormularEntry } from '../models/entities';

@Injectable({
    providedIn: 'root',
})
export class FormularEntryService {
    private formularEntries: FormularEntry[] = [];

    public getFormularEntries(): Observable<FormularEntry[]> {
        return from([this.formularEntries]);
    }

    public bookFormularEntry(formularEntry: FormularEntry) {
        for (const debitBooking of formularEntry.debitAccounts) {
            debitBooking.account.debitSideAccountings.push(
                new AccountReference(
                    formularEntry.date,
                    formularEntry.creditAccounts.map(
                        (cb: FormularEntryBooking) => cb.account
                    ),
                    debitBooking.value,
                    debitBooking.side
                )
            );
        }

        for (const creditBooking of formularEntry.creditAccounts) {
            creditBooking.account.creditSideAccountings.push(
                new AccountReference(
                    formularEntry.date,
                    formularEntry.debitAccounts.map(
                        (cb: FormularEntryBooking) => cb.account
                    ),
                    creditBooking.value,
                    creditBooking.side
                )
            );
        }

        this.formularEntries.push(formularEntry);
    }
}
