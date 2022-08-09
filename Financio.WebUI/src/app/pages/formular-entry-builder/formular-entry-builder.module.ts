import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AutoCompleteModule } from 'primeng/autocomplete';
import { AvatarModule } from 'primeng/avatar';
import { ButtonModule } from 'primeng/button';
import { CalendarModule } from 'primeng/calendar';
import { CardModule } from 'primeng/card';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { MessageModule } from 'primeng/message';
import { TableModule } from 'primeng/table';
import { ToolbarModule } from 'primeng/toolbar';
import { SharedModule } from 'src/app/shared/shared.module';

import { FeBookingManagerComponent } from './fe-booking-manager/fe-booking-manager.component';
import { FormularEntryBuilderComponent } from './formular-entry-builder.component';

@NgModule({
    declarations: [FormularEntryBuilderComponent, FeBookingManagerComponent],
    imports: [
        CommonModule,
        FormsModule,
        SharedModule,
        ToolbarModule,
        ButtonModule,
        CardModule,
        AvatarModule,
        InputTextModule,
        CalendarModule,
        AutoCompleteModule,
        InputNumberModule,
        TableModule,
        MessageModule,
    ],
    exports: [FormularEntryBuilderComponent],
})
export class FormularEntryBuilderModule {}
