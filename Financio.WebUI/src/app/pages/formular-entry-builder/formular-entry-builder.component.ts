import {
    Component,
    OnInit,
    OnDestroy,
    ViewChildren,
    QueryList,
    ViewChild,
} from '@angular/core';
import { AccountService } from 'src/app/services/account.service';
import { Observable, switchMap, Subscription } from 'rxjs';
import { FormularEntryService } from '../../services/formular-entry.service';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import {
    Account,
    FormularEntryBooking,
    AccountSide,
    FormularEntry,
} from '../../models/entities';
import { FeBookingManagerComponent } from './fe-booking-manager/fe-booking-manager.component';
import { NgForm } from '@angular/forms';
import { NavigationService } from '../../services/navigation.service';

@Component({
    selector: 'app-formular-entry-builder',
    templateUrl: './formular-entry-builder.component.html',
    styleUrls: ['./formular-entry-builder.component.scss'],
})
export class FormularEntryBuilderComponent implements OnInit, OnDestroy {
    @ViewChild('form', { static: false }) private form: NgForm | null = null;
    @ViewChildren(FeBookingManagerComponent)
    private bookingManagers: QueryList<FeBookingManagerComponent> | null = null;

    public date = new Date();
    public notes = '';

    public debitSideBookings: FormularEntryBooking[] = [
        { account: null!, value: 1, side: AccountSide.Debit },
    ];
    public creditSideBookings: FormularEntryBooking[] = [
        {
            account: null!,
            value: 1,
            side: AccountSide.Credit,
        },
    ];

    public accounts$: Observable<Account[]> = null!;

    private initSide: AccountSide = AccountSide.Debit;
    private getAccountSubscritpion: Subscription = null!;

    constructor(
        private accountService: AccountService,
        private formularEntryService: FormularEntryService,
        private router: Router,
        private activatedRoute: ActivatedRoute,
        private navigation: NavigationService
    ) {}

    public get isValid(): boolean {
        return (
            (this.bookingManagers
                ?.map((bm) => bm.isValid)
                ?.reduce((a, b) => a && b) ??
                true) &&
            this.sumOfValuesAreEqual &&
            (this.form?.valid ?? false)
        );
    }

    public get sumOfValuesAreEqual(): boolean {
        return (
            this.debitSideBookings
                .map((b) => b.value)
                .reduce((a, b) => a + b) ===
            this.creditSideBookings.map((b) => b.value).reduce((a, b) => a + b)
        );
    }

    public ngOnInit() {
        this.getAccountSubscritpion = this.activatedRoute.paramMap
            .pipe(
                switchMap((paramMap: ParamMap) => {
                    this.initSide =
                        (paramMap.get('side') as AccountSide) ??
                        AccountSide.Debit;
                    return this.accountService.findByName(paramMap.get('name'));
                })
            )
            .subscribe((account) => {
                if (account.length === 0) return;

                (this.initSide === AccountSide.Debit
                    ? this.debitSideBookings
                    : this.creditSideBookings)[0].account = account[0];
            });
    }

    public ngOnDestroy() {
        this.getAccountSubscritpion.unsubscribe();
    }

    public onCancel() {
        // this.router.navigate(['']);
        this.navigation.goBack();
    }

    public onSave() {
        const fe = new FormularEntry(this.date, this.notes);

        for (const booking of this.debitSideBookings) {
            fe.debitAccounts.push(booking);
        }

        for (const booking of this.creditSideBookings) {
            fe.creditAccounts.push(booking);
        }

        this.formularEntryService.bookFormularEntry(fe);
        // this.router.navigate(['']);
        this.navigation.goBack();
    }
}
