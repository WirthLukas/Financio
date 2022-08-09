import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountOverviewModule } from './pages/account-overview/account-overview.module';
import { AccountEditModule } from './pages/account-edit/account-edit.module';
import { FormularEntryBuilderModule } from './pages/formular-entry-builder/formular-entry-builder.module';
import { MenuModule } from 'primeng/menu';
import { BookingOverviewModule } from './pages/booking-overview/booking-overview.module';
import { ConfigModule } from './pages/config/config.module';

@NgModule({
    declarations: [AppComponent],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        FormsModule,
        AppRoutingModule,
        AccountOverviewModule,
        AccountEditModule,
        FormularEntryBuilderModule,
        MenuModule,
        BookingOverviewModule,
        ConfigModule,
    ],
    providers: [],
    bootstrap: [AppComponent],
})
export class AppModule {}
