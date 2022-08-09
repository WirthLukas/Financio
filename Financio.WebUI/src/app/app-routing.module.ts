import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountOverviewComponent } from './pages/account-overview/account-overview.component';
import { AccountEditComponent } from './pages/account-edit/account-edit.component';
import { FormularEntryBuilderComponent } from './pages/formular-entry-builder/formular-entry-builder.component';
import { BookingOverviewComponent } from './pages/booking-overview/booking-overview.component';
import { ConfigComponent } from './pages/config/config.component';

const routes: Routes = [
    { path: 'account-overview', component: AccountOverviewComponent },
    { path: 'account-edit/:number', component: AccountEditComponent },
    {
        path: 'formular-entry-builder',
        component: FormularEntryBuilderComponent,
    },
    { path: 'booking-overview', component: BookingOverviewComponent },
    { path: 'config', component: ConfigComponent },
    { path: '', redirectTo: '/account-overview', pathMatch: 'full' },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}
