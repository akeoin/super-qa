import { Injectable } from '@angular/core';
import { Router, CanActivate } from '@angular/router';
import { AppSessionService } from '../session/app-session.service';

@Injectable()
export class AppRouteGuard implements CanActivate {
    constructor(
        private _router: Router,
        private _sessionService: AppSessionService
    ) { }

    canActivate(): boolean {
        if (this._sessionService.user) {
            return true;
        }

        this._router.navigate(['/account/login']);
        return false;
    }
}
