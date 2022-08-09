import { DOCUMENT } from '@angular/common';
import { Inject, Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class ThemeService {
    private _theme: string | null = null;

    get theme(): string {
        let themeLink = this.document.getElementById(
            'app-theme'
        ) as HTMLLinkElement | null;

        if (themeLink === null) {
            themeLink = this.createThemeLink('lara-light-blue');
        }

        const splittedUrl = themeLink.href.split('/');
        return splittedUrl[splittedUrl.length - 1].split('.')[0];
    }

    constructor(@Inject(DOCUMENT) private document: Document) {}

    switchTheme(theme: string) {
        const themeLink = this.document.getElementById(
            'app-theme'
        ) as HTMLLinkElement | null;

        if (themeLink) {
            themeLink.href = `${theme}.css`;
        } else {
            this.createThemeLink(theme);
        }
    }

    private createThemeLink(themeName: string): HTMLLinkElement {
        const themeLink = this.document.createElement('link');
        themeLink.type = 'text/css';
        themeLink.rel = 'stylesheet';
        themeLink.href = `${themeName}.css`;
        themeLink.id = 'app-theme';

        this.document.head.appendChild(themeLink);
        return themeLink;
    }
}
