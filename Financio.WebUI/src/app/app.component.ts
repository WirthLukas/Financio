import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { AccountService } from './services/account.service';
import { ThemeService } from './services/theme.service';
import { NavigationService } from './services/navigation.service';

/*
  - Settings Page => Dark/Light Theme Changer       => done
  - Bookings Overview Page                          => done
  - Overall Layout konfiguration (Sakai Theme Layout.css)   => done
  - Routing State Manager with Breadcrumb on each page      => done
  - Connect to Backend
*/

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
    public menuItems: MenuItem[] = [];

    constructor(
        private accountService: AccountService,
        private themeService: ThemeService,
        private navigationService: NavigationService
    ) {}

    ngOnInit() {
        this.menuItems = [
            {
                label: 'Accounts',
                icon: 'pi pi-list',
                routerLink: [''],
                command: (_) => this.navigationService.clearHistory(),
            },
            {
                label: 'Bookings',
                icon: 'pi pi-book',
                routerLink: ['booking-overview'],
                command: (_) => this.navigationService.clearHistory(),
            },
            {
                label: 'Config',
                icon: 'pi pi-cog',
                routerLink: ['config'],
                command: (_) => this.navigationService.clearHistory(),
            },
        ];

        this.navigationService.startSaveHistory();

        if (!this.themeService.theme) throw new Error('No Theme created!');
    }
}
