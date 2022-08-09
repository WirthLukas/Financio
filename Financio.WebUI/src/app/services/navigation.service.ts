import { Location } from '@angular/common';
import { Injectable } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class NavigationService {
    private _history: string[] = [];
    private navigationEndSubscription: Subscription | null = null;

    public get history(): string[] {
        return this._history;
    }

    public get previousUrl(): string | null {
        if (this._history.length <= 0) {
            return null;
        }

        return this._history[this._history.length - 2]; // -2 because the last element is the current url
    }

    constructor(private router: Router, private location: Location) {}

    public startSaveHistory() {
        this.navigationEndSubscription = this.router.events.subscribe(
            (event) => {
                if (event instanceof NavigationEnd) {
                    if (
                        this._history.length > 0 &&
                        this._history[this._history.length - 1] ===
                            event.urlAfterRedirects
                    ) {
                        return;
                    }

                    this._history.push(event.urlAfterRedirects);
                }
            }
        );
    }

    public endSaveHistory() {
        this.navigationEndSubscription?.unsubscribe();
    }

    public goBack() {
        this._history.pop();

        if (this._history.length > 0) {
            this.location.back();
        } else {
            this.router.navigate(['']);
        }
    }

    public clearHistory() {
        this._history = [];
    }
}
