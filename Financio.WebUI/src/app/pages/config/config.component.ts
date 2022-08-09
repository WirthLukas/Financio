import { Component, OnInit } from '@angular/core';
import { ThemeService } from '../../services/theme.service';

@Component({
    selector: 'app-config',
    templateUrl: './config.component.html',
    styleUrls: ['./config.component.scss'],
})
export class ConfigComponent implements OnInit {
    public themes = ['lara-light-blue', 'lara-dark-blue'];
    public selectedTheme: string = null!;

    constructor(private themeService: ThemeService) {}

    public ngOnInit() {
        this.selectedTheme = this.themeService.theme;
    }

    public changeTheme(theme?: string) {
        this.themeService.switchTheme(theme ?? this.selectedTheme);
    }
}
