import { Component, OnInit, ViewChild } from '@angular/core';
import { Account } from 'src/app/models/entities';
import { AccountService } from 'src/app/services/account.service';
import { Nullable } from '../../utils/types';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import { AccountDetailsComponent } from '../../shared/account-details/account-details.component';
import { ConfirmationService, MessageService } from 'primeng/api';

@Component({
  selector: 'app-account-overview',
  templateUrl: './account-overview.component.html',
  styleUrls: ['./account-overview.component.scss'],
  providers: [MessageService, ConfirmationService],
})
export class AccountOverviewComponent implements OnInit {
  public accountName: string = '';
  public accounts$: Observable<Account[]> = null!; // Gets set in NgOnInit
  public currentAccount: Nullable<Account> = null;

  public accountDialog = false;
  public newAccount = new Account('', '');

  @ViewChild(AccountDetailsComponent)
  private accountDetails!: AccountDetailsComponent;

  constructor(
    private accountService: AccountService,
    private router: Router,
    private messageService: MessageService,
    private confirmationService: ConfirmationService
  ) {}

  public ngOnInit() {
    this.accounts$ = this.accountService.getAll();
  }

  public searchName() {
    this.accounts$ = this.accountService.findByName(this.accountName);
  }

  public onAccountSelect(account: Account) {
    this.currentAccount = account;
  }

  public onAccountUnselect() {
    this.currentAccount = null;
  }

  public onEditAccount() {
    this.router.navigate(['account-edit', this.currentAccount?.number ?? '']);
  }

  public onDeleteAccount() {
    this.confirmationService.confirm({
      message: `Are you sure you want to delete ${this.currentAccount?.number} ${this.currentAccount?.name}?`,
      header: 'Confirm',
      icon: 'pi pi-exclamation-triangle',
      accept: () => this.deleteAccount(),
    });
  }

  private deleteAccount() {
    if (this.currentAccount) {
      this.accountService.deleteAccount(this.currentAccount);
      this.accounts$ = this.accountService.getAll();
      this.messageService.add({
        severity: 'success',
        summary: 'Account Deleted',
        detail: `Deleted Account ${this.currentAccount.number} ${this.currentAccount.name}`,
      });
      this.currentAccount = null;
    }
  }

  public onNewAccount() {
    this.accountDialog = true;
    this.newAccount = new Account('', '');
  }

  public saveAccount() {
    if (this.accountDetails.isValid) {
      this.accountService.addAccount(this.newAccount);
      this.hideDialog();
      this.messageService.add({
        severity: 'success',
        summary: 'Account Created',
        detail: `Created new Account ${this.newAccount.number} ${this.newAccount.name}`,
      });
    } else {
      this.messageService.add({
        severity: 'error',
        summary: 'Invalid Data',
        detail: 'Not all inputs are correct',
      });
    }
  }

  public hideDialog() {
    this.accountDialog = false;
  }
}
