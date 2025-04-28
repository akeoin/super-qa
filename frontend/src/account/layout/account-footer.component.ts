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
    const releaseDate = this.appSession?.application?.releaseDate;
    
    // Handle both string and Date formats
    let formattedDate = '';
    if (releaseDate) {
      if (typeof releaseDate === 'string') {
        // If it's a string, try to parse it
        const date = new Date(releaseDate);
        if (!isNaN(date.getTime())) {
          formattedDate = `${date.getFullYear()}${String(date.getMonth() + 1).padStart(2, '0')}${String(date.getDate()).padStart(2, '0')}`;
        }
      } else if (releaseDate instanceof Date) {
        formattedDate = `${releaseDate.getFullYear()}${String(releaseDate.getMonth() + 1).padStart(2, '0')}${String(releaseDate.getDate()).padStart(2, '0')}`;
      }
    }

    // If we couldn't format the date, use current date
    if (!formattedDate) {
      const now = new Date();
      formattedDate = `${now.getFullYear()}${String(now.getMonth() + 1).padStart(2, '0')}${String(now.getDate()).padStart(2, '0')}`;
    }

    this.versionText = version + ' [' + formattedDate + ']';
  }
}
