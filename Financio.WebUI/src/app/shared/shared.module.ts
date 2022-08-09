import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountDetailsComponent } from './account-details/account-details.component';
import { FormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { MessageModule } from 'primeng/message';
import { NavigationHistoryComponent } from './navigation-history/navigation-history.component';
import { BreadcrumbModule } from 'primeng/breadcrumb';

@NgModule({
    declarations: [AccountDetailsComponent, NavigationHistoryComponent],
    imports: [
        CommonModule,
        FormsModule,
        InputTextModule,
        InputTextareaModule,
        MessageModule,
        BreadcrumbModule,
    ],
    exports: [AccountDetailsComponent, NavigationHistoryComponent],
})
export class SharedModule {}
