import {
    Component,
    EventEmitter,
    Input,
    OnInit,
    Output,
    ViewChild,
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/services/account.service';
import {
    Account,
    AccountSide,
    FormularEntryBooking,
} from '../../../models/entities';

@Component({
    selector: 'app-fe-booking-manager',
    templateUrl: './fe-booking-manager.component.html',
    styleUrls: ['./fe-booking-manager.component.scss'],
})
export class FeBookingManagerComponent {
    @Input() public title = 'Title';
    @Input() public bookings: FormularEntryBooking[] = [];
    @Input() public side: 'debit' | 'credit' = 'debit';

    @Output() public bookingsChange = new EventEmitter<
        FormularEntryBooking[]
    >();

    @ViewChild('form', { static: false }) private form!: NgForm;

    public selectedBookings: FormularEntryBooking[] = [];
    public accounts$: Observable<Account[]> = null!;

    constructor(private accountService: AccountService) {}

    public get isValid(): boolean {
        return this.form?.valid ?? true;
    }

    public get sumOfValues(): number {
        return this.bookings.map((b) => b.value).reduce((a, b) => a + b);
    }

    public accountSearch(query: string) {
        this.accounts$ = this.accountService.findByNameOrNumber(query);
    }

    public onSelect(account: Account, booking: FormularEntryBooking) {
        booking.account = account;
    }

    public addBooking() {
        this.bookings.push({
            account: null!,
            value: 1,
            side: this.side === 'debit' ? AccountSide.Debit : AccountSide.Debit,
        });
    }

    public deleteBookings() {
        this.bookings = this.bookings.filter(
            (b) => !this.selectedBookings.includes(b)
        );
        this.bookingsChange.emit(this.bookings);
        this.selectedBookings = [];
    }

    public isNotCheckable(booking: FormularEntryBooking): boolean {
        return (
            this.bookings.length === 1 ||
            (!this.selectedBookings.includes(booking) &&
                this.selectedBookings.length + 1 === this.bookings.length)
        );
    }
}
