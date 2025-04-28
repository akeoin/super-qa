import { Injectable } from '@angular/core';
import { AbpSessionService } from 'abp-ng2-module';
import { SessionServiceProxy } from '@shared/service-proxies/service-proxies';

@Injectable()
export class AppSessionService {
    private _user: any;
    private _tenant: any;
    private _application: any;

    constructor(
        private _abpSessionService: AbpSessionService,
        private _sessionService: SessionServiceProxy
    ) {
        this.init();
    }

    get application(): any {
        return this._application;
    }

    get user(): any {
        return this._user;
    }

    get userId(): number {
        return this._user?.id;
    }

    get tenant(): any {
        return this._tenant;
    }

    get tenantId(): number {
        return this._tenant?.id;
    }

    getShownLoginName(): string {
        const userName = this._user?.userName;
        if (!this._abpSessionService.tenantId) {
            return userName;
        }

        return (this._tenant ? this._tenant.tenancyName : '.') + '\\' + userName;
    }

    init(): Promise<boolean> {
        return new Promise<boolean>((resolve, reject) => {
            this._sessionService.getCurrentLoginInformations().subscribe(
                (result) => {
                    this._application = result.application;
                    this._user = result.user;
                    this._tenant = result.tenant;
                    resolve(true);
                },
                (err) => {
                    reject(err);
                }
            );
        });
    }
}
