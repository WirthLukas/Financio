import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AvatarModule } from 'primeng/avatar';
import { TableModule } from 'primeng/table';
import { ToolbarModule } from 'primeng/toolbar';
import { BookingOverviewComponent } from './booking-overview.component';
import { CardModule } from 'primeng/card';
import { ButtonModule } from 'primeng/button';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
    declarations: [BookingOverviewComponent],
    imports: [
        CommonModule,
        FormsModule,
        SharedModule,
        ToolbarModule,
        AvatarModule,
        CardModule,
        TableModule,
        ButtonModule,
    ],
    exports: [BookingOverviewComponent],
})
export class BookingOverviewModule {}
