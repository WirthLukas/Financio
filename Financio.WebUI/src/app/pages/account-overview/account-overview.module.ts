import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AvatarModule } from 'primeng/avatar';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';

import { SharedModule } from 'src/app/shared/shared.module';

import { AccountOverviewComponent } from './account-overview.component';

@NgModule({
    declarations: [AccountOverviewComponent],
    imports: [
        CommonModule,
        FormsModule,
        SharedModule,
        TableModule,
        ButtonModule,
        CardModule,
        InputTextModule,
        ToolbarModule,
        DialogModule,
        ToastModule,
        ConfirmDialogModule,
        AvatarModule,
    ],
    exports: [AccountOverviewComponent],
})
export class AccountOverviewModule {}
