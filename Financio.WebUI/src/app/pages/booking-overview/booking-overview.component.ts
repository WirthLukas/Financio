import { Component, OnInit } from '@angular/core';
import { FormularEntryService } from '../../services/formular-entry.service';
import { Observable } from 'rxjs';
import { FormularEntry } from 'src/app/models/entities';
import { Router } from '@angular/router';

@Component({
    selector: 'app-booking-overview',
    templateUrl: './booking-overview.component.html',
    styleUrls: ['./booking-overview.component.scss'],
})
export class BookingOverviewComponent implements OnInit {
    public bookings$: Observable<FormularEntry[]> = null!;

    constructor(
        private formularEntryService: FormularEntryService,
        private router: Router
    ) {}

    ngOnInit(): void {
        this.bookings$ = this.formularEntryService.getFormularEntries();
    }

    public onNewBooking() {
        this.router.navigate(['formular-entry-builder']);
    }
}
