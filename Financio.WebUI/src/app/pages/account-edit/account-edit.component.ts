import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Account } from 'src/app/models/entities';
import { AccountService } from '../../services/account.service';
import { AccountReference, AccountSide } from '../../models/entities';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { switchMap, Subscription } from 'rxjs';
import { AccountDetailsComponent } from '../../shared/account-details/account-details.component';
import { MessageService } from 'primeng/api';
import { NavigationService } from '../../services/navigation.service';

@Component({
    selector: 'app-account-edit',
    templateUrl: './account-edit.component.html',
    styleUrls: ['./account-edit.component.scss'],
    providers: [MessageService],
})
export class AccountEditComponent implements OnInit, OnDestroy {
    public accountNumber: string = '';
    public account: Account | undefined;

    private accountBackup: Account | undefined; // if edit screen gets canceled
    private accountSupscription: Subscription = null!;
    @ViewChild(AccountDetailsComponent)
    private accountDetails!: AccountDetailsComponent;

    constructor(
        private accountService: AccountService,
        private activatedRoute: ActivatedRoute,
        private router: Router,
        private messageService: MessageService,
        private navigation: NavigationService
    ) {}

    public ngOnInit() {
        this.accountSupscription = this.activatedRoute.paramMap
            .pipe(
                switchMap((paramMap: ParamMap) => {
                    this.accountNumber = paramMap.get('number') ?? '';
                    return this.accountService.getByNumber(this.accountNumber);
                })
            )
            .subscribe((account: Account | undefined) => {
                this.account = account;
                this.accountBackup = this.account?.duplicate();
            });
    }

    public ngOnDestroy() {
        this.accountSupscription.unsubscribe();
    }

    public onSave() {
        if (this.accountDetails.isValid) {
            // TODO: cannot show message because we navigate back
            // this.router.navigate(['']);
            this.navigation.goBack();
        } else {
            this.messageService.add({
                severity: 'error',
                summary: 'Invalid Data',
                detail: 'Not all inputs are correct',
            });
        }
    }

    public onCancel() {
        if (this.accountBackup) {
            this.account?.copy(this.accountBackup);
        }

        // this.router.navigate(['']);
        this.navigation.goBack();
    }

    public onAddBooking(side: 'debit' | 'credit') {
        this.router.navigate([
            'formular-entry-builder',
            {
                name: this.account?.name,
                side: side === 'debit' ? AccountSide.Debit : AccountSide.Credit,
            },
        ]);
    }

    public getAllAccountReferences(account: Account): AccountReference[] {
        return account.debitSideAccountings.concat(
            account.creditSideAccountings
        );
    }
}
