import { Injectable } from '@angular/core';
import { AbpSessionService } from 'abp-ng2-module';

@Injectable()
export class AppSessionService {
    constructor(private _abpSessionService: AbpSessionService) {}

    get application(): any {
        return (window as any).abp?.application;
    }

    get user(): any {
        return (window as any).abp?.session?.user;
    }

    get userId(): number {
        return (window as any).abp?.session?.userId;
    }

    get tenant(): any {
        return (window as any).abp?.session?.tenant;
    }

    get tenantId(): number {
        return (window as any).abp?.session?.tenantId;
    }

    getShownLoginName(): string {
        const userName = this.user?.userName;
        if (!this._abpSessionService.tenantId) {
            return userName;
        }

        return (this.tenant ? this.tenant.tenancyName : '.') + '\\' + userName;
    }

    init(): Promise<boolean> {
        return new Promise<boolean>((resolve) => {
            resolve(true);
        });
    }
}
