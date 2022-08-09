import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { NavigationService } from '../../services/navigation.service';

@Component({
    selector: 'app-navigation-history',
    templateUrl: './navigation-history.component.html',
    styleUrls: ['./navigation-history.component.scss'],
})
export class NavigationHistoryComponent implements OnInit {
    public items: MenuItem[] = [];
    public home: MenuItem = null!;

    constructor(private navigationService: NavigationService) {}

    ngOnInit(): void {
        this.navigationService.history.forEach((url) =>
            this.items.push({ label: url })
        );

        this.home = {
            icon: 'pi pi-home',
            routerLink: [''],
            command: (_) => this.navigationService.clearHistory(),
        };
    }
}
