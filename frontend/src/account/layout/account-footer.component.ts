import { Component, Injector, ChangeDetectionStrategy } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';

@Component({
  selector: 'account-footer',
  templateUrl: './account-footer.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AccountFooterComponent extends AppComponentBase {
  currentYear: number;
  versionText: string;

  constructor(injector: Injector) {
    super(injector);

    this.currentYear = new Date().getFullYear();
    const version = this.appSession?.application?.version || '1.0.0';
    const releaseDate = this.appSession?.application?.releaseDate || new Date();
    const formattedDate = `${releaseDate.getFullYear()}${String(releaseDate.getMonth() + 1).padStart(2, '0')}${String(releaseDate.getDate()).padStart(2, '0')}`;
    this.versionText = version + ' [' + formattedDate + ']';
  }
}
