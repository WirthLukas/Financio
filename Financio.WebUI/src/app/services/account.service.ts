import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { filter, from, map, Observable, switchMap } from 'rxjs';
import { Account } from '../models/entities';

@Injectable({
    providedIn: 'root',
})
export class AccountService {
    constructor(private readonly httpClient: HttpClient) {}

    // public findByName(name: string | null): Observable<Account[]> {
    //     this.accounts ??= this.httpClient.get<Account[]>(
    //         'localhost:4600/account'
    //     );

    //     return name === null
    //         ? from([])
    //         : this.accounts.pipe(
    //               map((accounts) =>
    //                   accounts.filter((a) => a.name.includes(name))
    //               )
    //           );
    // }

    public getAll(): Observable<Account[]> {
        return this.httpClient.get<Account[]>('https://localhost:7063/Account');
    }
}
