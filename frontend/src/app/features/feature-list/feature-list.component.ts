import { Component, OnInit, Injector, ChangeDetectorRef } from '@angular/core';
import { FeatureService } from '../feature.service';
import { Feature } from '../models/feature.dto';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { CreateOrEditFeatureComponent } from '../create-or-edit-feature/create-or-edit-feature.component';

class PagedFeaturesRequestDto extends PagedRequestDto {
  keyword: string;
}

@Component({
  selector: 'app-feature-list',
  templateUrl: './feature-list.component.html',
  styleUrls: ['./feature-list.component.css'],
  animations: [appModuleAnimation()]
})
export class FeatureListComponent extends PagedListingComponentBase<Feature> implements OnInit {
  features: Feature[] = [];
  keyword = '';
  isTableLoading = false;

  constructor(
    private featureService: FeatureService,
    private _modalService: BsModalService,
    injector: Injector,
    cd: ChangeDetectorRef
  ) {
    super(injector, cd);
  }

  ngOnInit(): void {
    this.refresh();
  }

  createFeature(): void {
    this.showCreateOrEditFeatureDialog();
  }

  editFeature(feature: Feature): void {
    this.showCreateOrEditFeatureDialog(feature.id);
  }

  delete(feature: Feature): void {
    abp.message.confirm(
      this.l('FeatureDeleteWarningMessage', feature.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this.featureService.deleteFeature(feature.id).subscribe(() => {
            abp.notify.success(this.l('SuccessfullyDeleted'));
            this.refresh();
          });
        }
      }
    );
  }

  private showCreateOrEditFeatureDialog(id?: number): void {
    let createOrEditFeatureDialog: BsModalRef;
    if (!id) {
      createOrEditFeatureDialog = this._modalService.show(
        CreateOrEditFeatureComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditFeatureDialog = this._modalService.show(
        CreateOrEditFeatureComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditFeatureDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }

  list(
    request: PagedFeaturesRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    this.isTableLoading = true;
    request.keyword = this.keyword;

    this.featureService
      .getFeatures(request.keyword, request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          this.isTableLoading = false;
          finishedCallback();
        })
      )
      .subscribe(
        (result) => {
          this.features = result.items;
          this.showPaging(result, pageNumber);
        },
        error => {
          console.error('Error loading features:', error);
          this.notify.error(this.l('ErrorLoadingFeatures'));
        }
      );
  }

  getStatusText(isActive: boolean): string {
    return isActive ? this.l('Active') : this.l('Inactive');
  }
} 