import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AvatarModule } from 'primeng/avatar';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';

import { SharedModule } from 'src/app/shared/shared.module';

import { AccountEditComponent } from './account-edit.component';

@NgModule({
    declarations: [AccountEditComponent],
    imports: [
        CommonModule,
        FormsModule,
        CardModule,
        TableModule,
        ButtonModule,
        ToolbarModule,
        ToastModule,
        SharedModule,
        AvatarModule,
    ],
    exports: [AccountEditComponent],
})
export class AccountEditModule {}
