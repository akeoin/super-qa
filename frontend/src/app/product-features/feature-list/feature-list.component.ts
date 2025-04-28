import { Component, OnInit, Injector, ChangeDetectorRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FeatureService } from '../feature.service';
import { Feature, PagedResultDto } from '../models/feature.model';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';

class PagedFeaturesRequestDto extends PagedRequestDto {
    keyword: string;
    status: string;
    parentFeatureId: number;
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
    status = '';
    parentFeatureId: number;
    isTableLoading = false;
    isDeleting = false;

    constructor(
        private featureService: FeatureService,
        private _router: Router,
        private route: ActivatedRoute,
        injector: Injector,
        cd: ChangeDetectorRef
    ) {
        super(injector, cd);
    }

    ngOnInit(): void {
        this.pageSize = 10;
        this.refresh();
    }

    createFeature(): void {
        this._router.navigate(['/app/product-features/create']);
    }

    editFeature(feature: Feature): void {
        this._router.navigate(['edit', feature.id], { relativeTo: this.route });
    }

    delete(feature: Feature): void {
        abp.message.confirm(
            this.l('FeatureDeleteWarningMessage', feature.name),
            undefined,
            (result: boolean) => {
                if (result) {
                    this.isDeleting = true;
                    this.featureService.deleteFeature(feature.id)
                        .pipe(
                            finalize(() => {
                                this.isDeleting = false;
                                this.cd.detectChanges();
                            })
                        )
                        .subscribe(
                            () => {
                                this.notify.success(this.l('SuccessfullyDeleted'));
                                this.refresh();
                            },
                            error => {
                                console.error('Error deleting feature:', error);
                                this.notify.error(this.l('ErrorDeletingFeature'));
                            }
                        );
                }
            }
        );
    }

    list(
        request: PagedFeaturesRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {
        this.isTableLoading = true;
        request.keyword = this.keyword;
        request.status = this.status;
        request.parentFeatureId = this.parentFeatureId;

        this.featureService
            .getFeatures(
                request.keyword,
                request.status,
                request.parentFeatureId,
                request.skipCount,
                request.maxResultCount
            )
            .pipe(
                finalize(() => {
                    this.isTableLoading = false;
                    finishedCallback();
                    this.cd.detectChanges();
                })
            )
            .subscribe(
                (result: PagedResultDto) => {
                    this.features = result.items;
                    this.showPaging(result, pageNumber);
                },
                error => {
                    console.error('Error loading features:', error);
                    this.notify.error(this.l('ErrorLoadingFeatures'));
                }
            );
    }

    getStatusText(status: string): string {
        switch (status) {
            case 'active':
                return this.l('Active');
            case 'inactive':
                return this.l('Inactive');
            default:
                return this.l('Unknown');
        }
    }
} 