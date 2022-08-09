import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AvatarModule } from 'primeng/avatar';
import { CardModule } from 'primeng/card';
import { DropdownModule } from 'primeng/dropdown';
import { ToolbarModule } from 'primeng/toolbar';

import { ConfigComponent } from './config.component';

@NgModule({
    declarations: [ConfigComponent],
    imports: [
        CommonModule,
        FormsModule,
        ToolbarModule,
        AvatarModule,
        CardModule,
        DropdownModule,
    ],
    exports: [ConfigComponent],
})
export class ConfigModule {}
