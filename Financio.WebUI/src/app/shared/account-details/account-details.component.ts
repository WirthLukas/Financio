import { Component, Input, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Account } from 'src/app/models/entities';

@Component({
    selector: 'app-account-details',
    templateUrl: './account-details.component.html',
    styleUrls: ['./account-details.component.scss'],
})
export class AccountDetailsComponent {
    @Input() public account: Account = new Account('', '', '');

    @ViewChild('form', { static: false }) private form!: NgForm;

    public get isValid(): boolean {
        return this.form.valid ?? false;
    }
}
